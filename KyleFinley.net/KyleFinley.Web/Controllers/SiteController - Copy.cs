using _928.Commands;
using _928.Core;
using _928.Entities;
using _928.Web;
using _928.Web.Mvc;
using _928.Web.MVC;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KyleFinley.Web.Controllers {
    public abstract class SiteController : BaseController {

        protected static string site = "KyleFinley.net";
        protected static string twitterHandle;

        public SiteController(ICommandDispatcher dispatcher)
            : base(dispatcher) {
        }

        private BaseViewData SetSiteViewDataProperties(BaseViewData viewData) {

            viewData.Site = site;
            viewData.TwitterHandle = twitterHandle;

            return viewData;
        }

        private T SetSiteViewDataProperties<T>(T viewData)
            where T : BaseViewData {

            viewData.Site = site;
            viewData.TwitterHandle = "kfinley";

            return viewData;
        }

        public T CreateViewDataOld<T>()
            where T : BaseViewData, new() {

            T viewData = new T {
                IsUserAuthenticated = IsUserAuthenticated,
                CurrentUser = CurrentUser
            };
            
            SetSiteViewDataProperties<T>(viewData);

            base.SetViewDataCanonical("", false, viewData);

            return viewData;
        }

        public override T CreateViewData<T>() {

            T viewData = base.CreateViewData<T>();

            return SetSiteViewDataProperties<T>(viewData);
        }

        public override T CreateViewData<T>(Entity entity) {

            T viewData = base.CreateViewData<T>(entity);

            try {
                viewData.PageImage = (entity as Page).PageImage;
            } catch (Exception) { }

            SetSiteViewDataProperties(viewData);

            if (viewData.Entity.Enabled == false) {
                viewData.NoIndex = true;
            }

            return viewData;
        }

        public ViewData<Entity> CreateViewData(Entity entity) {

            var viewDataType = typeof(ViewData<>).MakeGenericType(entity.GetType());

            var method = this.GetType()
                .GetMethods()
                .Where(m => (m.Name == "CreateViewData"
                    && m.IsGenericMethod
                    && m.GetParameters().Length == 1)
                    && m.GetParameters()[0].ParameterType == typeof(Entity))
                .First();
         
            method = method.MakeGenericMethod(viewDataType);

            var viewData = method.Invoke(this, new Object[] { entity });

            try {
                ((BaseViewData)viewData).PageImage = (entity as Page).PageImage;
            } catch (Exception) { }

            SetSiteViewDataProperties(viewData as BaseViewData);

            return viewData as ViewData<Entity>;
        }

        public BaseViewData SetProperties(BaseViewData viewData) {
           
            try {
                viewData.PageImage = ((viewData as BaseViewData<IEntity>).Entity as Page).PageImage;
            } catch (Exception) { }
            
            return SetSiteViewDataProperties(viewData);
        }

        protected ViewResult View(object model, bool minify = true, bool skipCanonicalCheck = false) {
            if (skipCanonicalCheck) {
                this.GetCanonicalUrl((BaseViewData)model);
            }
            try {
                SetSiteViewDataProperties((BaseViewData)model);

            
            return base.View(model.GetType().GetProperty("Entity").PropertyType.Name, model); 
                       } catch (Exception) {
                
            }

            return base.View(model);
        }

        protected ActionResult View<T>(ViewData<T> model, bool minify = true, bool skipCanonicalCheck = false)
            where T : Entity {

            if (skipCanonicalCheck == false) {
                this.GetCanonicalUrl(model);
            }

            return base.View(model as BaseViewData, minify, skipCanonicalCheck);

        }

        protected new ActionResult View(BaseViewData model, bool minify = true, bool skipCanonicalCheck = false) {

            if (skipCanonicalCheck == false) {
                this.GetCanonicalUrl(model);
            }

            return base.View(model, minify, skipCanonicalCheck);
        }

        protected ActionResult View(string view, BaseViewData model, bool minify = true) {

            this.GetCanonicalUrl(model);

            return base.View(view, model, minify);
        }

        //protected ActionResult View(string view, ViewData<Entity> model, bool minify = true) {

        //    return this.View(view, (BaseViewData)model, minify);
        //}

        protected override void HandleUnknownAction(string actionName) {

            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            var errorRoute = new RouteData();
            errorRoute.Values.Add("controller", "Error");
            errorRoute.Values.Add("action", "PageNotFound");
            errorRoute.Values.Add("url", HttpContext.Request.Url.OriginalString);

            var viewData = this.CreateViewDataOld<ErrorViewData>();
            viewData.NoIndex = true;

            View("PageNotFound", viewData).ExecuteResult(this.ControllerContext);

        }

        private void GetCanonicalUrl(BaseViewData model) {

            if (model.Id != Guid.Empty) {
                var getUrl = CommandFactory.Create<GetSiteUrl>();
                getUrl.EntityId = model.Id;

                dispatcher.Run(getUrl);

                model.Canonical = getUrl.Data.Path;
            }
        }

        private void GetCanonicalUrl<T>(ViewData<T> model)
            where T : Entity {

            if (model.Id != Guid.Empty) {
                var getUrl = CommandFactory.Create<GetSiteUrl>();
                getUrl.EntityId = model.Id;

                dispatcher.Run(getUrl);

                model.Canonical = getUrl.Data.Path;
            }
        }
    }
}