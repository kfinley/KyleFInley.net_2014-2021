using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _928.Core;
using KyleFinley.Commands;
using KyleFinley.Web.Models;
using KyleFinley.Models;
using _928.Commands;
using _928.Web.Mvc;


namespace KyleFinley.Web.Controllers
{
    public class ArticleController : SiteController
    {

        public ArticleController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: Article
        public ActionResult Index(Guid id)
        {
            // Setup Commands
            var getArticle = CommandFactory.Create<GetArticle>();
            getArticle.Id = id;

            var getUrl = CommandFactory.Create<GetSiteUrl>();
            getUrl.EntityId = id;

            // Run Commands
            dispatcher.Run(getArticle);
            dispatcher.Run(getUrl);

            // Return Result
            var viewData = base.CreateViewDataOld<ViewData<Article>>();
            viewData.Entity = getArticle.Data;
            viewData.Canonical = getUrl.Data.Path;
            viewData.PageImage = getArticle.Data.PageImage;
            viewData.PublishedDate = getArticle.Data.PublishedDate;
            viewData.ModifiedDate = getArticle.Data.LastModified;

            if (viewData.Entity.Enabled == false) {
                viewData.NoIndex = true;
            }

            return View(viewData);
        }
    }
}