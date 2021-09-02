using _928.Commands;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _928.Web.Mvc;
using _928.Web;
using _928.Entities.Models;

namespace KyleFinley.Web.Controllers.Manage
{
    [Authorize]
    public class CategoriesController : ManagementController {

        public CategoriesController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        public ActionResult Index() {

            // Setup Commmands
            var getCategories = CommandFactory.Create<GetPages<Category>>();
            getCategories.EntityType = (int)EntityType.Category;
            getCategories.EntityCommand = CommandFactory.Create<GetCategories>();

            // Run Commands
            dispatcher.Run(getCategories);

            // Return Result
            var viewData = base.CreateViewData<ListViewData<Page<Category>>>();
            viewData.Items = getCategories.Data.ToList(); //.OrderByDescending(a => a.PublishedDate).ToList();        
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }

        [HttpGet]
        public ActionResult Create() {

            var viewData = base.CreateViewData<AuthorViewData<Category>>();
            viewData.AuthorMode = AuthorMode.Create;
            viewData.Canonical = Url.Action(this.Action());

            return View("Author", viewData);
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {

            // Setup Commands
            var getCategory = CommandFactory.Create<GetPage<Category>>();
            getCategory.Id = id;
            getCategory.RetrieveShareUrlStats = true;
            getCategory.EntityCommand = CommandFactory.Create<GetCategory>();

            // Run Commands
            dispatcher.Run(getCategory);
            
            // Return Result
            var viewData = base.CreateViewData<AuthorViewData<Category>>();
            viewData.Page = getCategory.Data;
            viewData.Url = getCategory.Data.Path;
            viewData.Canonical = Url.Action(this.Action(), new { id = id });

            viewData.AuthorMode = AuthorMode.Review;
            viewData.Saved = TempData["saved"] != null ? (bool)TempData["saved"] : false;
            
            
            return View("Author", viewData, false);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AuthorViewData<Category> edit) {

            // Setup Commands
            var getCategory = CommandFactory.Create<GetPage<Category>>();
            getCategory.Id = edit.Page.Id;
            getCategory.EntityCommand = CommandFactory.Create<GetCategory>();

            // Run Commands - Retrieve article to edit
            dispatcher.Run(getCategory);
            
            var category = getCategory.Data;

            category.Title = edit.Page.Title;
            category.Description = edit.Page.Description;
            category.Content = edit.Page.Content;
            category.Enabled = edit.Page.Enabled;
            category.PageImage = edit.Page.PageImage;
            
            // Setup Commands
            var editCategory = CommandFactory.Create<EditPage<Category>>();
            editCategory.Data = category;
            editCategory.Site = base.Site;
            editCategory.Url = category.Path;

            // Run Commands
            dispatcher.Run(editCategory);

            TempData["saved"] = true;

            // Return Result
            return RedirectToAction("Edit", new { id = editCategory.Data.Id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AuthorViewData<Category> newCategory) {

            // Setup Commands
            var create = CommandFactory.Create<CreatePage<Category>>();
            create.Data = newCategory.Page;
            
            create.Site = base.Site;
            create.Url = newCategory.Url;

            // Run Commands 
            dispatcher.Run(create, create.UnitOfWork.Commit);

            TempData["saved"] = true;

            // Return Result
            return RedirectToAction("Edit", new { id = create.Data.Id });

        }
    }

}