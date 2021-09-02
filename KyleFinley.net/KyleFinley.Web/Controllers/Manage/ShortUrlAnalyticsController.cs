using _928.Commands;
using _928.Core;
using _928.Entities;
using _928.Entities.Models;
using _928.UrlShortener;
using _928.Web.Mvc;
using KyleFinley.Commands;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KyleFinley.Web.Controllers.Manage
{
    public class ShortUrlAnalyticsController : ManagementController
    {
        
        public ShortUrlAnalyticsController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: ShortUrlAnalytics
        public ActionResult Index(string id)
        {
            // Create Commands
            var getAnalytics = CommandFactory.Create<GetShortUrlAnalytics>();
            getAnalytics.ShortUrlKey = id;

            // Run Commands
            dispatcher.Run(getAnalytics);

            // Return Result
            var viewData = base.CreateViewData<ViewData<ShortUrlAnalytics>>();
            viewData.Page.Entity = getAnalytics.Data;

            return View(viewData);
        }
    }
}