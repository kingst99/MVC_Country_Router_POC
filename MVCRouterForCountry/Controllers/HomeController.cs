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
            ViewBag.Message = "Your country code is : " + CountryCode;

            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your country code is : " + CountryCode;

            return View();
        }

        public ActionResult Contact()
        {
            
            ViewBag.Message = "Your country code is : " + CountryCode;

            return View();
        }
    }
}