using _928.Commands;
using _928.Core.Interfaces;
using _928.Entities;
using _928.Web.Mvc;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace KyleFinley.Web.Controllers {
    public class ContentController : SiteController {
      
        public ContentController(ICommandDispatcher dispatcher, IHttpContext httpContext)
            : base(dispatcher, httpContext) {
        }

        public ActionResult Home(Guid id, Guid entityId) {

            // Setup Commands
            var getHomePage = CommandFactory.Create<GetPage<Home>>();
            getHomePage.Id = id;
            getHomePage.RetrieveShareUrlStats = false;

            getHomePage.EntityCommand = CommandFactory.Create<GetHome>();

            var getHeaderImage = CommandFactory.Create<GetHeaderImage>();
            getHeaderImage.Type = HeaderImageType.Large;

            var getLatestArticles = CommandFactory.Create<GetLatestArticlesList>();

            // Run Commands 
            dispatcher.Run(getHeaderImage);
            dispatcher.Run(getLatestArticles);
            dispatcher.Run(getHomePage);

            // Return Results
            var viewData = base.CreateViewData<HomeViewData>();
            viewData.Page = getHomePage.Data;
            viewData.HeaderImage = getHeaderImage.Data.FileName;
            viewData.PageImage = getHeaderImage.Data.FileName;
            viewData.ArticleSummaries = getLatestArticles.Data;
            
            return View(viewData);
        }

        public ActionResult Article(Guid id, Guid entityId) {
            
            // Setup Commands
            var getArticle = CommandFactory.Create<GetPage<Article>>();
            getArticle.Id = id;
            getArticle.EntityCommand = CommandFactory.Create<GetArticle>();
            
            // Run Commands
            dispatcher.Run(getArticle);
            
            // Return Result
            var articleViewData = base.CreateViewData<ViewData<Article>>();
            articleViewData.Page = getArticle.Data;
            articleViewData.PageImage = getArticle.Data.PageImage;
            articleViewData.PublishedDate = getArticle.Data.PublishedDate;
            articleViewData.ModifiedDate = getArticle.Data.LastModified;

            if (articleViewData.Page.Enabled == false) {
                articleViewData.NoIndex = true;
            }

            return View("Article", articleViewData);
        }

        public ActionResult Category(Guid id, Guid entityId) {
          
            // Setup Commands
            var getCategory = CommandFactory.Create<GetPage<Category>>();
            getCategory.Id = id;

            var getArticles = CommandFactory.Create<GetCategoryArticles>();
            getArticles.Id = entityId;

            // Run Commands
            dispatcher.Run(getCategory);
            dispatcher.Run(getArticles);

            // Return Result
            var categoryViewData = new CategoryViewData {
                Page = getCategory.Data,
                ArticleSummaries = getArticles.Data
            };

            base.SetProperties(categoryViewData);

            return View("Category", categoryViewData);
        }
    }
}