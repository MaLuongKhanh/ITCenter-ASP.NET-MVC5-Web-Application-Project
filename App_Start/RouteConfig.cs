using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ForgetPassword", "{type}/{id}",
                new { controller = "Login", action = "ForgetPassword", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "quen-mat-khau" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("ResetPassword", "{type}/{id}",
                new { controller = "Login", action = "ResetPassword", id = UrlParameter.Optional},
                new RouteValueDictionary
                {
                    { "type", "reset-password" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("Login", "{type}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "dang-nhap" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("Register", "{type}/{id}",
                new { controller = "Login", action = "Register", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "dang-ky" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("ChangePassword", "{type}/{id}",
                new { controller = "Login", action = "ChangePassword", id = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "doi-mat-khau" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("GetOTP", "{type}/{meta}",
                new { controller = "Login", action = "GetOTP", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "quen-mat-khau" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("Profile", "{type}/{meta}",
                new { controller = "Profile", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "quan-li-trang-ca-nhan" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("Support", "{type}/{meta}",
                new { controller = "Support", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "tiep-nhan-ho-tro" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("ScheduleRegister", "{type}/{meta}",
                new { controller = "ScheduleRegister", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "dang-ky-ca-truc" }
                },
                new[] { "WebApplication.Controllers" });


            routes.MapRoute("Achievement", "{type}/{meta}",
                new { controller = "Achievement", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "thanh-tich" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("SoftwareStorage", "{type}/{meta}",
                new { controller = "SoftwareStorage", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "kho-phan-mem" }
                },
                new[] { "WebApplication.Controllers" });


            routes.MapRoute("Attendance", "{type}/{meta}",
                new { controller = "Attendance", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "diem-danh-ca-truc" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("Home", "{type}/{meta}",
                new { controller = "Default", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "trang-chu" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("AboutWeb", "{type}/{meta}",
                new { controller = "About", action = "AboutWeb", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "ve-trang-web" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("AboutUs", "{type}/{meta}",
                new { controller = "About", action = "AboutUs", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "ve-chung-toi" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute("LandingPage", "{type}/{meta}",
                new { controller = "LandingPage", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    { "type", "" }
                },
                new[] { "WebApplication.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",  
                defaults: new { controller = "LandingPage", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"WebApplication.Controllers"}
            );
        }
    }
}
