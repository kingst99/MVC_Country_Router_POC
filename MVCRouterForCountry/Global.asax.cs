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
            var Cookie_CountryCode = Utility.CookieSettings.ReadCountryCookie();

            string[] CountryList = Utility.GetAllowCountries();
            string RawUrl = HttpContext.Current.Request.RawUrl;

            string CountryCodeInUrl = Utility.GetCountryCodeInUrl(CountryList, RawUrl);

            if (CountryList.Contains(CountryCodeInUrl.ToUpper()))
            {
                if (Cookie_CountryCode != CountryCodeInUrl)
                {
                    Cookie_CountryCode = CountryCodeInUrl;

                    Utility.CookieSettings.SaveCountryCookie(Cookie_CountryCode);
                }

            }
        }

    }
}
