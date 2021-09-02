using _928.Web.Routing;
using KyleFinley.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KyleFinley.Web {
    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {

            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");
            routes.IgnoreRoute("scripts/{*pathInfo}");
            routes.IgnoreRoute("files/{*pathInfo}");

            routes.MapRoute(NamedRoutes.Robots, "robots.txt", new { controller = "GeneralContent", action = "Robots" });

            routes.MapRoute(
                name: NamedRoutes.PAGE_NOT_FOUND,
                url: "page-not-found",
                defaults: new { controller = "Error", action = "PageNotFound" }
            );

            routes.MapRoute(
                name: NamedRoutes.ERROR,
                url: "page-error",
                defaults: new { controller = "Error", action = "Error" }
            );

            routes.MapRoute(
                name: NamedRoutes.FORCE_ERROR,
                url: "force-error",
                defaults: new { controller = "Error", action = "Force" }
           );

            routes.MapRoute(
               name: NamedRoutes.Login,
               url: "Login",
               defaults: new { controller = "Account", action = "Login" }
           );

            routes.MapRoute(
                name: NamedRoutes.Account,
                url: "account/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(NamedRoutes.SiteMapXml, "sitemap.xml", new { controller = "GeneralContent", action = "Sitemap" });

            routes.MapRoute(NamedRoutes.Manage,
            "manage/{controller}/{action}/{id}",
            defaults: new { Controller = "Manage", action = "Index", id = UrlParameter.Optional }
           );


            routes.MapRoute(NamedRoutes.TopLevel,
              "{url}",
              new { action = "Index" }
              ).RouteHandler = new RouteHandler() {
                  UrlHandlerCommandType = typeof(HandleRouting)
              };

            //routes.MapRoute(NamedRoutes.ManageArticles,
            //    "manage/articles/{action}/{id}",
            //    defaults: new { Controller = "articles", action = "Index", id = UrlParameter.Optional }
            //);

            
            //routes.MapRoute(NamedRoutes.ManageUrls,
            //    "manage/urls/{action}/{id}",
            //    defaults: new { Controller = "urls", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(NamedRoutes.Home,
              "", new { url = "" }
              ).RouteHandler = new RouteHandler() {
                  UrlHandlerCommandType = typeof(HandleRouting)
              };

            //routes.MapRoute(NamedRoutes.Home,
            //   "",
            //   new { controller = "Content", action = "Home", id = Guid.Empty },
            //   new { IsRootAction = new IsRouteControllerConstraint() }
            //   );

           routes.LowercaseUrls = true;
        }
    }

    public struct NamedRoutes {
        public static string Home = "Home";
        public static string PAGE_NOT_FOUND = "PageNotFound";
        public static string ERROR = "Error";
        public static string FORCE_ERROR = "ForceError";
        public static string TopLevel = "TopLevel";
        public static string SecondLevel = "SecondLevel";
        public static string Manage = "Manage";
        public static string ManageArticles = "ManageArticles";
        public static string ManageUrls = "ManageUrls";
        //public static string ManageIndex = "ManageIndex";
        public static string Login = "Login";
        public static string Account = "Account";
        public static string SiteMapXml = "SiteMapXml";
        public static string Robots = "Robots";

        public static string ManageApi = "ManageApi";

    }
}
