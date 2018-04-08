using MVCRouterForCountry.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCRouterForCountry
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //讀取Cookie
            var Cookie_CountryCode = Utility.CookieSettings.ReadCountryCookie();

            //取得網站支援的所有語系代碼陣列 { "GLOBAL", "US", "TW" }
            string[] CountryList = Utility.GetAllowCountries();

            //取得訪客查詢的原始網址
            string RawUrl = HttpContext.Current.Request.RawUrl;

            //從訪客查詢網址判斷訪客欲使用之國家語系
            string CountryCodeInUrl = Utility.GetCountryCodeInUrl(CountryList, RawUrl);

            //利用陣列來檢查抓取的CountryCodeInUr語系代碼是否合法
            if (CountryList.Contains(CountryCodeInUrl.ToUpper()))
            {
                //先判斷Cookie上次紀錄的預設語系與本次查詢不同，不同則覆蓋，減少不必要的Cookie儲存動作
                if (Cookie_CountryCode != CountryCodeInUrl)
                {
                    Cookie_CountryCode = CountryCodeInUrl;

                    Utility.CookieSettings.SaveCountryCookie(Cookie_CountryCode);
                }
            }
            else
            {
                //網址未偵測到語系代碼(不合法)
                //訪客查詢網址不合法，將使用Cookie上次紀錄的預設語系並重新導向URL
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
                HttpContext.Current.Response.Redirect(redirectUrl, true);
            }
        }

    }
}
