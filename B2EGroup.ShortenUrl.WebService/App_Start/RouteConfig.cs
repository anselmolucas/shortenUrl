using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace B2EGroup.ShortenUrl.WebService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{url}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, url = UrlParameter.Optional }
            );            
        }
    }
}
