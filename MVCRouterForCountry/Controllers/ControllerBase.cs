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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.RawUrl.Length >= 2)
            {
                if (filterContext.HttpContext.Request.RawUrl.Substring(1, 2) != "")
                {
                    CountryCode = filterContext.HttpContext.Request.RawUrl.Substring(1, 2);
                }
            }
        }
    }
}