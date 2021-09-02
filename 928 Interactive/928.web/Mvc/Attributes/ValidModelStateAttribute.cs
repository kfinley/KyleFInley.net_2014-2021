using _928.Web.Mvc.Results;
using System;
using System.Web.Mvc;

namespace _928.Web.Mvc.Attributes
{
    /// <summary>
    /// An ActionFilter for automatically validating ModelState before a controller action is executed.
    /// Performs a Redirect if ModelState is invalid. Assumes the <see cref="ImportModelStateFromTempDataAttribute"/> is used on the GET action.
    /// Modified from source: https://github.com/benfoster/Fabrik.Common/blob/master/src/Fabrik.Common.Web/Filters/ValidateModelStateAttribute.cs
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateModelStateAttribute : ModelStateTempDataTransferAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url.Query.Contains("NoValidation"))
            {
                return;
            }

            var modelStateValid = filterContext.Controller.ViewData.ModelState.IsValid;

            if (!modelStateValid)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    ProcessAjax(filterContext);
                }
                else
                {
                    ProcessNormal(filterContext);
                }
            }

            base.OnActionExecuting(filterContext);
        }

        protected virtual void ProcessNormal(ActionExecutingContext filterContext)
        {
            // Export ModelState to TempData so it's available on next request
            ExportModelStateToTempData(filterContext);

            // redirect back to GET action
            filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
        }

        protected virtual void ProcessAjax(ActionExecutingContext filterContext)
        {
            var errors = filterContext.Controller.ViewData.ModelState.ToSerializableList();

            //send 400 status code (Bad Request)
            filterContext.Result = new JsonErrorResult
            {
                Data = new { Type = "Validation", Errors = errors }
            };
        }
    }
}
