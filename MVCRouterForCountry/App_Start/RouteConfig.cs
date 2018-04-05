using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCRouterForCountry
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //新增country路由節點，country可能包含 "GLOBAL", "US", "TW" 字串，預設'global'
            routes.MapRoute(
                name: "Country",
                url: "{country}/{controller}/{action}/{id}",
                defaults: new { country = "global", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //以下註解了原路由程式碼
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
