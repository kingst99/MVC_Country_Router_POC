using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRouterForCountry.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            //驗證目前語系測定結果
            ViewBag.Message = "Your country code is : " + UserCountryCode + ", Session = " + HttpContext.Session["CountryCode"] ?? "";

            return View();
        }

        public ActionResult About()
        {
            //驗證目前語系測定結果
            ViewBag.Message = "Your country code is : " + UserCountryCode + ", Session = " + HttpContext.Session["CountryCode"] ?? "";

            return View();
        }

        public ActionResult Contact()
        {
            //驗證目前語系測定結果
            ViewBag.Message = "Your country code is : " + UserCountryCode + ", Session = " + HttpContext.Session["CountryCode"] ?? "";

            return View();
        }
    }
}