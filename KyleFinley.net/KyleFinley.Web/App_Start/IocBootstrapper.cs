using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using StructureMap;

using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.UrlShortener;
using _928.Web.Mvc;

using KyleFinley.Commands;
using KyleFinley.Data;
using KyleFinley.Web.Models;


namespace KyleFinley.Web.App_Start {

    public static class IocBootstrapper {
        public static void Run() {

            try {
                var container = new Container(x => {

                    x.For<IHttpContext>().Use<_928.Web.HttpContextWrapper>();
                    
                    x.AddRegistry<DataRegistry>();
                    //x.AddRegistry<_928.Commands.CommandsRegistry>();
                    x.AddRegistry<CommandsRegistry>();

                    x.For<System.Data.Entity.DbContext>().Use(() => new ApplicationDbContext());
                    x.For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
                    x.For<UserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
                    x.For<UserManager<ApplicationUser>>().Use<ApplicationUserManager>();

                    x.For<IUrlShortenerService>().Use<GoogleUrlShortenerService>()
                    .Ctor<string>("apiKey").Is("AIzaSyDZhm5LlPLpK7GkHT9iob1Xp8I0tDTmE9k")
                    .Ctor<string>("appName").Is("KyleFinley.net")
                    .Ctor<Action<Exception, PolicyInfo>>("logAction").Is(new LogException(new _928.Web.HttpContextWrapper()).Log); ;

                });

                _928.Commands.CommandFactory.Initialize(container);

                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(container));

            } catch (Exception ex) {

#if DEBUG
                System.Diagnostics.Debug.Write(ex.ToString());
#endif
                // Logging code
                var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                    new PolicyInfo {
                        OriginalException = ex.GetType(),
                        Behavior = ExceptionPolicyBehavior.Replace,
                        NewException = typeof(HttpException),
                        Message = "Error creating IoC Container. Message: {0}".FormatWith(ex.Message)
                    }));

                handler.HandleException(ex);
            }
        }
    }
}