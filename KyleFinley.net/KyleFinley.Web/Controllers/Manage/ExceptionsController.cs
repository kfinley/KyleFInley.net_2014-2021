using _928.Core;
using KyleFinley.Commands;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _928.Web.Mvc;
using _928.Commands;
using _928.Entities;

namespace KyleFinley.Web.Controllers.Manage
{
    [Authorize]
    public class ExceptionsController : ManagementController
    {

        public ExceptionsController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: VanityUrls
        public ActionResult Index()
        {
            // Setup Commands
            var getUrls = CommandFactory.Create<GetExceptions>();

            // Run Commands
            dispatcher.Run(getUrls);

            // Return Result
            var viewData = base.CreateViewData<ListViewData<CoreException>>();
            viewData.Items = getUrls.Data;
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }
    }
}