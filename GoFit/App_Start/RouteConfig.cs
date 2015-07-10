using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoFit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = "Home|MyWorkouts|MyProfile|MyAccount|Exercise|FavoriteWorkouts|Comments|Error|AdminWorkouts|AdminTypes|AdminHome|AdminExercises|AdminCategories|AdminComments"}
            );
            routes.MapRoute(
                name: "ControllerCatchall",
                url: "{*any}",
                defaults: new { controller = "Error", action = "NotFoundError" }
            );
        }
    }
}
