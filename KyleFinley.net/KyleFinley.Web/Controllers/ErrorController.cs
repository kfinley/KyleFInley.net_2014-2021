
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _928.Web;
using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;


using KyleFinley.Commands;
using KyleFinley.Web.Models;

namespace KyleFinley.Web.Controllers {
    public class ErrorController : SiteController {

        public ErrorController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        public ActionResult Error(Exception exception) {

#if DEBUG

#endif
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;

            var log = CommandFactory.Create<LogException>();
            log.Exception = exception;
            log.AdditionalData = new { Request = this.HttpContext.Request.ToRaw() };
            dispatcher.Run(log);

            return View(new HandleErrorInfo(exception, "Error", "Error"));
        }

        public ActionResult PageNotFound(Exception exception) {

            var path = Request.Url.AbsolutePath;

            //if (Request.Url.Query.Contains("aspxerrorpath")) {
            //    // Handles asp.net CustomErrors which include the aspxerrorpath param
            //    path = Request["aspxerrorpath"];
            //} else {
            //    // Handles IIS httpErrors which passes the error as www.karmaloop.com/?404;http://www.domain.com/bad-url when the response mode is ExecuteURL
            //    path = "/" +
            //           Request.Url.Query.Substring(Request.Url.Query.IndexOf(Request.Url.Query.Split('/')[3], StringComparison.Ordinal));
            //}

            // Setup Commands
            var getRedirect = CommandFactory.Create<GetRedirect>();
            getRedirect.OldPath = path;

            // Run Commands
            dispatcher.Run(getRedirect);

            // Return result
            var redirect = getRedirect.Data;

            if (redirect.Do) {


                return RedirectPermanent(redirect.NewPath + (Request.Url.Query.HasValue() ? Request.Url.Query : string.Empty));
            } else {

                var log = CommandFactory.Create<LogException>();
                log.Exception = exception;
                log.AdditionalData = new { Request = this.HttpContext.Request.ToRaw() };
                dispatcher.Run(log);

                Response.StatusCode = 404;
                Response.TrySkipIisCustomErrors = true;

                var viewData = base.CreateViewData<ErrorViewData>();
                viewData.NoIndex = true;

                return View(viewData);
            }
        }

        public ActionResult Force() {
            throw new Exception("Error!!");
        }

    }
}