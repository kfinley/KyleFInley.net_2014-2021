using _928.Commands;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _928.Web.Mvc;

namespace KyleFinley.Web.Controllers.Manage
{
    [Authorize]
    public abstract class ManagementController : SiteController
    {

        public ManagementController(ICommandDispatcher dispatcher)
            : base(dispatcher)
        {

        }

        public override T CreateViewData<T>(Entity entity)
        {

            T viewData = base.CreateViewData<T>(entity);
            viewData.Canonical = Url.Action(this.Action());

            viewData.NoIndex = true;

            return viewData;
        }

        protected ActionResult View(string view, BaseViewData model)
        {
            return base.View(view, model, false, true);
        }
    }

    [Authorize]
    public abstract class ManagementApiController : BaseApiController
    {

        public ManagementApiController(ICommandDispatcher dispatcher)
            : base(dispatcher)
        {

        }
    }
}