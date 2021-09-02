using _928.Commands;
using _928.Core;
using _928.Entities;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KyleFinley.Web.Controllers {
    public class GeneralContentController : SiteController {

        public GeneralContentController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        public ActionResult Sitemap() {
            // setup commands
            var getSiteUrls = CommandFactory.Create<GetSitemapUrls>();

            // run commands
            dispatcher.Run(getSiteUrls);

            var viewData = base.CreateViewData<SitemapViewData>();
            viewData.Urls = getSiteUrls.Data;

            // return result
            Response.ContentType = "text/xml";
            return View(viewData, true, true);
        }

        public ActionResult Robots() {

            var viewData = base.CreateViewData<RobotsViewData>();

            viewData.Disallow = new List<Url>();
            viewData.Disallow.Add(new Url() { Path = "/manage/*" });
            viewData.Disallow.Add(new Url() { Path = "/login" });

            viewData.Allow = new List<Url>();
            
            Response.ContentType = "text/plain; charset=utf-8";
            return View(viewData, false, true);

        }
    }
}