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
            string CountryCodeInUrl = "";
            string[] CountryList = { "GLOBAL", "US", "TW" };

            var countryCode = CookieSettings.ReadCookie();

            if (HttpContext.Current.Request.RawUrl.Length >= 7)
            {
                CountryCodeInUrl = HttpContext.Current.Request.RawUrl.Substring(1, 6);

                if (!CountryList.Contains(CountryCodeInUrl.ToUpper()))
                {
                    if (HttpContext.Current.Request.RawUrl.Length >= 2)
                    {
                        CountryCodeInUrl = HttpContext.Current.Request.RawUrl.Substring(1, 2);
                    }
                }
            }
            else
            {
                if (HttpContext.Current.Request.RawUrl.Length >= 2)
                {
                    CountryCodeInUrl = HttpContext.Current.Request.RawUrl.Substring(1, 2);
                }
            }

                if (CountryList.Contains(CountryCodeInUrl.ToUpper()))
            {
                if (countryCode != CountryCodeInUrl)
                {
                    countryCode = CountryCodeInUrl;

                    CookieSettings.SaveCookie(countryCode);
                }

            }
        }

        public class CookieSettings
        {
            public static void SaveCookie(string data)
            {
                var mfdCookie = new HttpCookie("CountryCookie");
                mfdCookie.Value = data;
                mfdCookie.Expires = DateTime.Now.AddDays(300);
                HttpContext.Current.Response.Cookies.Add(mfdCookie);
            }

            public static string ReadCookie()
            {
                var mfdCookieValue = "";
                if (HttpContext.Current.Request.Cookies["CountryCookie"] != null)
                    mfdCookieValue = HttpContext.Current.Request.Cookies["CountryCookie"].Value;
                return mfdCookieValue;
            }

        }
    }
}
