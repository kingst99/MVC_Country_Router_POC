using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRouterForCountry.Library
{
    public class Utility
    {

        public static string[] GetAllowCountries()
        {
            List<string> Countries = new List<string>() { "GLOBAL", "US", "TW" };
            return Countries.ToArray<string>();
        }

        public static class CookieSettings
        {
            public static void SaveCountryCookie(string data)
            {
                var mfdCookie = new HttpCookie("CountryCookie");
                mfdCookie.Value = data;
                mfdCookie.Expires = DateTime.Now.AddDays(300);
                HttpContext.Current.Response.Cookies.Add(mfdCookie);
            }

            public static string ReadCountryCookie()
            {
                var mfdCookieValue = "";
                if (HttpContext.Current.Request.Cookies["CountryCookie"] != null)
                    mfdCookieValue = HttpContext.Current.Request.Cookies["CountryCookie"].Value;
                return mfdCookieValue;
            }
        }

        public static string GetCountryCodeInUrl(string[] CountryList, string RawUrl)
        {
            string CountryCodeInUrl = "";

            if (RawUrl.Length >= 7)
            {
                CountryCodeInUrl = RawUrl.Substring(1, 6);

                if (!CountryList.Contains(CountryCodeInUrl.ToUpper()))
                {
                    if (RawUrl.Length >= 2)
                    {
                        CountryCodeInUrl = RawUrl.Substring(1, 2);
                    }
                }
            }
            else
            {
                if (RawUrl.Length >= 2)
                {
                    CountryCodeInUrl = RawUrl.Substring(1, 2);
                }
            }

            return CountryCodeInUrl;
        }
    }
}