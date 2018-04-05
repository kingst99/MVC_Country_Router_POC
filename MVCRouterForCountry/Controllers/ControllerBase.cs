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
        public string UserCountryCode = "";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] CountryList = Utility.GetAllowCountries();
            string RawUrl = filterContext.HttpContext.Request.RawUrl;

            UserCountryCode = Utility.GetCountryCodeInUrl(CountryList, RawUrl);

            if (!CountryList.Contains(UserCountryCode.ToUpper()))
            {
                var Cookie_CountryCode = Utility.CookieSettings.ReadCountryCookie();
                var redirectUrl = "/global";
                if (Cookie_CountryCode != "")
                {
                    redirectUrl = string.Format("/{0}", Cookie_CountryCode);
                }
                filterContext.HttpContext.Response.Redirect(redirectUrl, true);
            }
            else
            {
                if (filterContext.HttpContext.Session["CountryCode"] != null)
                {
                    if (!filterContext.HttpContext.Session["CountryCode"].Equals(UserCountryCode))
                    {
                        filterContext.HttpContext.Session.Abandon();
                        filterContext.HttpContext.Response.Redirect(RawUrl, true);
                    }
                }
                else
                {
                    filterContext.HttpContext.Session["CountryCode"] = UserCountryCode;
                }
            }
        }
    }
}