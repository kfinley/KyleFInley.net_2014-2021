using _928.Web.Mvc;
using _928.Commands;
using _928.Core.Linq;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _928.Web;
using _928.Entities.Models;

namespace KyleFinley.Web.Controllers.Manage
{
    public class HomeController : ManagementController
    {
        public HomeController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        public ActionResult Index()
        {
            // Setup Commmands
            var getPages = CommandFactory.Create<GetPages<Home>>();
            getPages.EntityType = (int)EntityType.Home;
            getPages.EntityCommand = CommandFactory.Create<GetHomes>();

            getPages.OrderBy(new OrderByExpression<Page<Home>, DateTime>(a => a.CreatedDate, SortOrder.Descending));

            // Run Commands
            dispatcher.Run(getPages);

            // Return Result
            var viewData = base.CreateViewData<ListViewData<Page<Home>>>();
            viewData.Items = getPages.Data.ToList();
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }

        [HttpGet]
        public ActionResult Edit(Guid id) {

            // Setup Commands
            var getHome = CommandFactory.Create<GetPage<Home>>();
            getHome.Id = id;
            getHome.RetrieveShareUrlStats = true;
            getHome.EntityCommand = CommandFactory.Create<GetHome>();
            
            var getUrl = CommandFactory.Create<GetUrl>();
            getUrl.Id = id;

            // Run Commands
            dispatcher.Run(getHome);
            dispatcher.Run(getUrl);

            // Return Result
            var viewData = base.CreateViewData<AuthorViewData<Home>>();

            viewData.Page = getHome.Data;
            viewData.Url = getUrl.Data.Path;
            viewData.AuthorMode = AuthorMode.Review;
            viewData.PageType = "Home";
            viewData.Saved = TempData["saved"] != null ? (bool)TempData["saved"] : false;
            viewData.Canonical = Url.Action(this.Action(), new { id = id });

            return View("Author", viewData);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AuthorViewData<Home> edit) {

            // Setup Commands
            var getHome = CommandFactory.Create<GetPage<Home>>();
            getHome.Id = edit.Page.Id;

            // Run Commands - Retrieve Home Page to edit
            dispatcher.Run(getHome);
            
            var home = getHome.Data;

            home.Title = edit.Page.Title;
            home.Description = edit.Page.Description;
            home.Content = edit.Page.Content;
            home.Enabled = edit.Page.Enabled;
            home.PageImage = edit.Page.PageImage;

            // Setup Commands
            var editHome = CommandFactory.Create<EditPage<Home>>();
            editHome.Data = home;
            editHome.Site = base.Site;
            editHome.Url = home.Path;

            // Run Commands
            dispatcher.Run(editHome);

            TempData["saved"] = true;

            // Return Result
            return RedirectToAction("Edit", new { id = editHome.Data.Id });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AuthorViewData<Home> newHome) {

            // Setup Commands
            var createHome = CommandFactory.Create<CreatePage<Home>>();
            createHome.Data = newHome.Page;

            createHome.Site = base.Site;
            createHome.Url = newHome.Url;

            // Run Commands 
            dispatcher.Run(createHome, createHome.UnitOfWork.Commit);

            TempData["saved"] = true;

            // Return Result
            return RedirectToAction("Edit", new { id = createHome.Data.Id });

        }

    }
}