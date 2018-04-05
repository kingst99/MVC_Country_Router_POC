using MVCRouterForCountry.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRouterForCountry.Controllers
{
    public class ControllerBase : Controller
    {
        //目前語系測定結果
        public string UserCountryCode = "";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //取得網站支援的所有語系代碼陣列 { "GLOBAL", "US", "TW" }
            string[] CountryList = Utility.GetAllowCountries();

            //取得訪客查詢的原始網址
            string RawUrl = filterContext.HttpContext.Request.RawUrl;

            //從訪客查詢網址判斷訪客欲使用之國家語系
            UserCountryCode = Utility.GetCountryCodeInUrl(CountryList, RawUrl);

            //利用陣列來檢查RawUrl抓取的CountryCodeInUr語系代碼
            if (!CountryList.Contains(UserCountryCode.ToUpper()))
            {
                //網址未偵測到語系代碼(不合法)
                //訪客查詢網址不合法，將使用Cookie上次紀錄的預設語系並重新導向URL
                var Cookie_CountryCode = Utility.CookieSettings.ReadCountryCookie();

                var redirectUrl = "";
                if (Cookie_CountryCode != "")
                {
                    redirectUrl = string.Format("/{0}", Cookie_CountryCode);
                }
                else
                {
                    //若Cookie沒有記錄，則導向預設語系global
                    redirectUrl = "/global";
                }

                //重新導向URL
                filterContext.HttpContext.Response.Redirect(redirectUrl, true);
            }
            else
            {
                //網址語系代碼有偵測到(合法)
                //若切換語系時並不需要清空Session，請註解以下程式碼:
                //------------------判斷是否存清空Session---------------
                //先檢查是否已存在Session資料
                if (filterContext.HttpContext.Session["CountryCode"] != null)
                {
                    //判斷Session上語系與訪客查詢網址的語系，不一樣時清空Session並重新導向
                    if (!filterContext.HttpContext.Session["CountryCode"].Equals(UserCountryCode))
                    {
                        //清空Session並重新導向到目前相同訪問網址
                        filterContext.HttpContext.Session.Abandon();
                        filterContext.HttpContext.Response.Redirect(RawUrl, true);
                    }
                }
                else
                {
                    //清空Session並重新導向之後，將新的語系代碼存入Session，Action將能正確讀取目前語系代碼
                    filterContext.HttpContext.Session["CountryCode"] = UserCountryCode;
                }
                //----------------------------------------------------------
            }
        }
    }
}