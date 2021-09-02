using System;
using System.Web.Mvc;

using _928.Core;
using _928.Web.Mvc;
using _928.Commands;
using _928.Entities.Models;
using _928.Web.Mvc.Attributes;

using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using _928.Web.Mvc.Results;

namespace KyleFinley.Web.Controllers.Manage
{

    [Authorize]
    public class ArticlesController : ManagementController
    {

        public ArticlesController(ICommandDispatcher dispatcher)
            : base(dispatcher)
        {

        }

        public ActionResult Index()
        {
            // Setup Commmands
            var getArticles = CommandFactory.Create<GetPages<Article>>();
            getArticles.EntityType = (int)EntityType.Article;
            getArticles.EntityCommand = CommandFactory.Create<GetArticles>();
            
            // Run Commands
            dispatcher.Run(getArticles);

            // Return Result
            var viewData = base.CreateViewData<ListViewData<Page<Article>>>();
            viewData.Items = getArticles.Data;
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewData = base.CreateViewData<AuthorViewData<Article>>();
            viewData.AuthorMode = AuthorMode.Create;
            viewData.Canonical = Url.Action(this.Action());

            return View("Author", viewData);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            // Setup Commands
            var getArticle = CommandFactory.Create<GetPage<Article>>();
            getArticle.Id = id;
            getArticle.RetrieveShareUrlStats = true;
            getArticle.EntityCommand = CommandFactory.Create<GetArticle>();

            // Run Commands
            dispatcher.Run(getArticle);

            // Return Result
            var viewData = base.CreateViewData<AuthorViewData<Article>>();
            viewData.Page = getArticle.Data;
            viewData.Url = getArticle.Data.Path;
            viewData.Canonical = Url.Action(this.Action(), new { id = id });

            viewData.AuthorMode = AuthorMode.Review;
            viewData.Saved = TempData["saved"] != null ? (bool)TempData["saved"] : false;

            return View("Author", viewData);
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateInput(false)]
        public JsonResult Edit(AuthorViewData<Article> edit)
        {
            try
            {
                // Setup Commands
                var editArticle = CommandFactory.Create<EditPage<Article>>();
                editArticle.Data = edit.Page;
                editArticle.Site = base.Site;
                editArticle.Url = edit.Page.Path;

                // Run Commands
                dispatcher.Run(editArticle);
                
                // Return Result
                return new JsonResult { Data = new { Success = true, Id = edit.Id, Message = "Article Saved." } };

            }
            catch (Exception ex)
            {
                var data = new { Type = "Exception", Exception = ex.InnerException.HasValue() ? ex.InnerException.Message : ex.Message };
                return new JsonErrorResult() { Data = data };
            }
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateInput(false)]
        public JsonResult Create(AuthorViewData<Article> newArticle)
        {
            try
            {
                // Setup Commands
                var createArticle = CommandFactory.Create<CreatePage<Article>>();
                createArticle.Data = newArticle.Page;

                createArticle.Data.Entity.Headline = newArticle.Page.Title;
                createArticle.Data.Entity.AlternativeHeadline = newArticle.Page.Description;
                createArticle.Data.Entity.Author = "Kyle Finley";

                createArticle.Site = base.Site;
                createArticle.Url = newArticle.Url;

                // Run Commands 
                dispatcher.Run(createArticle, createArticle.UnitOfWork.Commit);

                TempData["saved"] = true;

                // Return Result
                return new JsonRedirectResult() { Redirect = Url.Action("Edit", new { id = createArticle.Data.Id }) };
            }
            catch (Exception ex)
            {
                var data = new { Type = "Exception", Exception = ex.InnerException.HasValue() ? ex.InnerException.Message : ex.Message };
                return new JsonErrorResult() { Data = data };
            }
        }
    }
}