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
            string CountryCodeInUrl = "", redirectUrl = "";
            var countryCode = CookieSettings.ReadCookie();
            if (countryCode == "")
            {
                countryCode = "global";
            }

            if (HttpContext.Current.Request.RawUrl.Length >= 2)
            {
                CountryCodeInUrl = HttpContext.Current.Request.RawUrl.Substring(1, 2);
            }

            if (countryCode != CountryCodeInUrl)
            {
                if (HttpContext.Current.Request.RawUrl.Length >= 2)
                {
                    if (HttpContext.Current.Request.RawUrl.Substring(1, 2) != "")
                    {
                        countryCode = HttpContext.Current.Request.RawUrl.Substring(1, 2);
                    }
                }

                if (!System.Web.HttpContext.Current.Request.RawUrl.Contains(countryCode))
                {
                    redirectUrl = string.Format("/{0}{1}", countryCode, System.Web.HttpContext.Current.Request.RawUrl);

                    System.Web.HttpContext.Current.Response.RedirectPermanent(redirectUrl);
                }
                else
                {
                    redirectUrl = System.Web.HttpContext.Current.Request.RawUrl;
                }
                CookieSettings.SaveCookie(countryCode);
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
