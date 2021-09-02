using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using _928.Core;
using _928.Web.Authentication;
using System.Web;
using System.Collections.Specialized;
using _928.Commands;
using _928.Security;
using _928.Security.Commands;
using _928.Entities;
using _928.Core.Interfaces;
using System.Web.Http;
using _928.Entities.Models;

namespace _928.Web.Mvc {

    public abstract class BaseController : Controller {

        //TODO: Fix this to inject the ApplicationContextWrapper
        protected ICommandDispatcher dispatcher = new CommandDispatcher(new HttpContextWrapper());
        private string site = "KyleFinley.net";
        private string twitterHandle = "kfinley";
       
        private static readonly Type CurrentUserKey = typeof(IUser);

        public BaseController()
            : base() {

        }

        public BaseController(ICommandDispatcher dispatcher) {
            this.dispatcher = dispatcher;
            FormsAuthentication = new FormsAuthenticationWrapper();

        }

        public BaseController(ICommandDispatcher dispatcher, IHttpContext httpContext) {
            this.dispatcher = dispatcher;
            this.dispatcher.HttpContext = httpContext;

            FormsAuthentication = new FormsAuthenticationWrapper();

        }

        public IFormsAuthentication FormsAuthentication {
            get;
            set;
        }

        public string CurrentUserName {
            get {
                return (HttpContext.User == null) ? null : HttpContext.User.Identity.Name;
            }
        }

        public IUser CurrentUser {
            get {

                if (!string.IsNullOrEmpty(CurrentUserName)) {
                    IUser user = System.Web.HttpContext.Current.Items[CurrentUserKey] as IUser;

                    if (user == null) {

                        var getUser = CommandFactory.Create<GetCurrentUser>();
                        dispatcher.Run(getUser, false);

                        user = getUser.Data;

                        if (user != null) {

                            //try
                            //{
                            //    if (!user.IsLockedOut)
                            //    {
                            //        user.LastActivityAt = SystemTime.Now();

                            //    }
                            //}
                            //catch (Exception e)
                            //{
                            //   // Log.Exception(e);
                            //}

                            HttpContext.Items[CurrentUserKey] = user;
                        }
                    }

                    return user;
                }

                return null;
            }
        }

        public bool IsUserAuthenticated {
            get {
                if (HttpContext.User.Identity.IsAuthenticated && (CurrentUser != null)) {
                    return true;

                    //if (!CurrentUser.IsLockedOut)
                    //{
                    //    return true;
                    //}

                    //Log.Warning("Logging out User: {0}", CurrentUserName);

                    // Logout the user if the account is locked out
                    //FormsAuthentication.SignOut();

                    //Log.Info("User Logged out.");
                }

                return false;
            }
        }

        protected T SetSiteViewDataProperties<T>(T viewData)
           where T : BaseViewData {

            viewData.Site = this.Site;
            viewData.TwitterHandle = this.TwitterHandle;

            return viewData;
        }

        protected virtual T CreateViewDataOld<T>(string routeName = "", bool removeController = false)
            where T : BaseViewData, new() {

            T viewData = new T {
                IsUserAuthenticated = IsUserAuthenticated,
                CurrentUser = CurrentUser
            };

            SetSiteViewDataProperties<T>(viewData);
            SetViewDataCanonical(routeName, removeController, viewData);

            return viewData;
        }

        protected virtual T CreateViewData<T>()
          where T : BaseViewData, new() {

              return this.CreateViewDataOld<T>();

            //T viewData = new T {
            //    IsUserAuthenticated = IsUserAuthenticated,
            //    CurrentUser = CurrentUser
            //};

            //SetSiteViewDataProperties<T>(viewData);

            //SetViewDataCanonical("", false, viewData);

            //return viewData;
        }

        //public virtual T CreateViewData<T>()
        //    where T : ViewData<Entity>, new() {

        //    return this.CreateViewData<T>("", false);
        //}

        public virtual T CreateViewData<T>(string routeName = "", bool removeController = false)
          where T : ViewData<Entity>, new() {

            T viewData = new T {
                IsUserAuthenticated = IsUserAuthenticated,
                CurrentUser = CurrentUser
            };

            SetSiteViewDataProperties<T>(viewData);
            SetViewDataCanonical(routeName, removeController, viewData);

            return viewData;

        }

        public virtual T CreateViewData<T>(Entity entity)
            where T : ViewData<Entity>, new() {

            return this.CreateViewData<T>(entity, "", false);
        }

        public virtual T CreateViewData<T>(Entity entity, string routeName = "", bool removeController = false)
            where T : ViewData<Entity>, new() {

            T viewData = new T {
                IsUserAuthenticated = IsUserAuthenticated,
                CurrentUser = CurrentUser
            };

            viewData.Page.Entity = entity;

            SetViewDataCanonical(routeName, removeController, viewData);

            return viewData;
        }

        public void SetViewDataCanonical(string routeName, bool removeController, BaseViewData viewData) {
            if (routeName != string.Empty) {
                if (removeController) {
                    var routeData = this.ControllerContext.RouteData.Values.ToList().Where(rd => rd.Key.ToLower() != "controller").ToDictionary(t => t.Key, t => t.Value); // .ToDictionary(t => t.Key);

                    viewData.Canonical = Url.RouteUrl(routeName, new System.Web.Routing.RouteValueDictionary(routeData));
                } else {
                    viewData.Canonical = Url.RouteUrl(routeName, this.ControllerContext.RouteData.Values);
                }
            } else {
                if (removeController) {
                    var routeData = this.ControllerContext.RouteData.Values.ToList().Where(rd => rd.Key.ToLower() != "controller").ToDictionary(t => t.Key, t => t.Value);
                    viewData.Canonical = Url.RouteUrl(new System.Web.Routing.RouteValueDictionary(routeData));
                } else {
                    viewData.Canonical = Url.RouteUrl(this.ControllerContext.RouteData.Values);
                }
            }
        }

        protected ActionResult View(BaseViewData model, bool minify = true, bool skipCanonicalCheck = false) {
            return InternalView(string.Empty, model, minify, skipCanonicalCheck);
        }

        protected ActionResult View(string view, BaseViewData model, bool minify = true, bool skipCanonicalCheck = false) {

            return this.InternalView(view, model, minify, skipCanonicalCheck);
        }

        private ActionResult InternalView(string view, BaseViewData model, bool minify = true, bool skipCanonicalCheck = false) {

            if (minify) {
#if !DEBUG
            Response.Filter = new MinifyFilter(Response.Filter);
#endif
            }

            if (skipCanonicalCheck == false &&
                Response.StatusCode == 200 &&
               Request.Url.PathAndQuery != "/" &&
               Request.Url.PathAndQuery.ToLower() != model.Canonical.ToLower()) {
                try {
                    if (IsRedirectRequired(Request.Url.PathAndQuery, model.Canonical)) {

                        return RedirectPermanent(HandleUtmParams(Request.Url.PathAndQuery, model.Canonical));
                    }

                } catch (Exception ex) {
                    var err = ex.Message;
                    return RedirectPermanent("/");
                }
            }

            if (view.HasValue())
                return base.View(view, model);
            else
                return base.View(model);
        }

        private bool IsRedirectRequired(string requestPathAndQuery, string canonical) {
            var approvedParams = new List<string> { "ReturnUrl", "utm_source", "utm_medium", "utm_term", "utm_content", "utm_campaign" };

            var requestQuery = requestPathAndQuery.Contains('?') ? requestPathAndQuery.Split('?')[1] : string.Empty;

            var requestQueryParams = HttpUtility.ParseQueryString(requestQuery);

            // Remove known approved params
            foreach (var param in approvedParams) {
                requestQueryParams.Remove(param);
            }

            var requestWithoutApprovedParams = requestQueryParams.Count > 0 ? String.Format("{0}?{1}", requestPathAndQuery.Split('?')[0], requestQueryParams.ToString()) : requestPathAndQuery.Split('?')[0];

            if (canonical.IsEmpty())
                canonical = "/";

            if (requestWithoutApprovedParams.ToLower() != canonical.ToLower()) {
                return true;
            }

            return false;
        }

        protected string Site {
            get {
                return this.site;
            }

            set {
                this.site = value;
            }
        }

        protected string TwitterHandle {
            get {
                return this.twitterHandle;
            }

            set {
                this.twitterHandle = value;
            }
        }
        
        private string HandleUtmParams(string requestPathAndQuery, string canonical) {
            var UtmParams = new List<string> { "utm_source", "utm_medium", "utm_term", "utm_content", "utm_campaign" };

            var requestQuery = requestPathAndQuery.Contains('?') ? requestPathAndQuery.Split('?')[1] : string.Empty;
            var requestQueryParams = HttpUtility.ParseQueryString(requestQuery);

            if (requestQueryParams.Count > 0) {
                var requestUtms = HttpUtility.ParseQueryString(string.Empty);

                foreach (var param in UtmParams) {
                    if (requestQueryParams.AllKeys.Contains(param))
                        requestUtms.Add(param, requestQueryParams[param]);
                }
                //return String.Format("{0}?{1}", canonical, requestUtms.ToString());
                return String.Format("{0}?{1}", canonical == string.Empty ? "/" : canonical, requestUtms.ToString());

            } else {
                return canonical;
            }
        }
    }
}
