using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRouterForCountry.Library
{
    /// <summary>
    /// 常用工具類
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 取得網站支援的所有語系代碼陣列
        /// </summary>
        /// <returns>語系代碼陣列</returns>
        public static string[] GetAllowCountries()
        {
            List<string> Countries = new List<string>() { "GLOBAL", "US", "TW" };
            return Countries.ToArray<string>();
        }

        /// <summary>
        /// Cookie工具類
        /// </summary>
        public static class CookieSettings
        {
            /// <summary>
            /// 寫入使用者慣用語系代碼
            /// </summary>
            /// <param name="data"></param>
            public static void SaveCountryCookie(string country_code)
            {
                var myCookie = new HttpCookie("CountryCookie");
                myCookie.Value = country_code;
                myCookie.Expires = DateTime.Now.AddDays(300);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }

            /// <summary>
            /// 讀取使用者慣用語系代碼，並返回結果
            /// </summary>
            /// <returns>語系代碼</returns>
            public static string ReadCountryCookie()
            {
                var mfdCookieValue = "";
                if (HttpContext.Current.Request.Cookies["CountryCookie"] != null)
                    mfdCookieValue = HttpContext.Current.Request.Cookies["CountryCookie"].Value;
                return mfdCookieValue;
            }
        }

        /// <summary>
        /// 解析工具，從訪問網址讀出代表語系代碼的根目錄名稱，並返回結果
        /// </summary>
        /// <param name="CountryList"></param>
        /// <param name="RawUrl"></param>
        /// <returns>網址內的語系代碼</returns>
        public static string GetCountryCodeInUrl(string[] CountryList, string RawUrl)
        {
            string CountryCodeInUrl = "";

            //若路徑超過長度，先判斷是否包含較長的語系代碼'global'
            if (RawUrl.Length >= 7)
            {
                CountryCodeInUrl = RawUrl.Substring(1, 6);

                //若沒有再判斷是否包含較短的語系代碼'tw'和'us'
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
                //路徑短時直接判斷是否包含較短的語系代碼'tw'和'us'
                if (RawUrl.Length >= 2)
                {
                    CountryCodeInUrl = RawUrl.Substring(1, 2);
                }
            }

            return CountryCodeInUrl;
        }
    }
}