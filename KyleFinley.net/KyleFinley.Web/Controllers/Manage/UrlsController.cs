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

namespace KyleFinley.Web.Controllers.Manage
{
    [Authorize]
    public class UrlsController : ManagementController
    {
        
        public UrlsController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: VanityUrls
        public ActionResult Index()
        {
            // Setup Commands
            var getUrls = CommandFactory.Create<GetUrls>();

            // Run Commands
            dispatcher.Run(getUrls);

            // Return Result
            var viewData = base.CreateViewData<UrlsViewData>();
            viewData.Urls = getUrls.Data;
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }
    }
}