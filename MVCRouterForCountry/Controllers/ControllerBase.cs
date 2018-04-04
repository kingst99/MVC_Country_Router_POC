using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRouterForCountry.Controllers
{
    public class ControllerBase : Controller
    {
        public string CountryCode = "";
        string[] CountryList = { "GLOBAL", "US", "TW" };

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.RawUrl.Length >= 7)
            {
                CountryCode = filterContext.HttpContext.Request.RawUrl.Substring(1, 6);
                
                if (!CountryList.Contains(CountryCode.ToUpper()))
                {
                    if (filterContext.HttpContext.Request.RawUrl.Length >= 2)
                    {
                        CountryCode = filterContext.HttpContext.Request.RawUrl.Substring(1, 2);
                    }
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.RawUrl.Length >= 2)
                {
                    CountryCode = filterContext.HttpContext.Request.RawUrl.Substring(1, 2);
                }
            }

            if (!CountryList.Contains(CountryCode.ToUpper()))
            {
                throw new HttpException(404, "Resource Not Found.");
            }
        }
    }
}