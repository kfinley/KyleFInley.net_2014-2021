using _928.Commands;
using _928.Web.MVC;
using KyleFinley.Commands;
using KyleFinley.Models;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KyleFinley.Web.Controllers
{
    public class CategoryController : SiteController, ISiteController
    {

        public CategoryController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: Category
        public ActionResult Index(Guid id) {
            // Setup Commands
            var getCategory = CommandFactory.Create<GetCategory>();
            getArticle.Id = id;

            var getUrl = CommandFactory.Create<GetUrl>();
            getUrl.EntityId = id;

            // Run Commands
            dispatcher.Run(getArticle);
            dispatcher.Run(getUrl);

            // Return Result
            var viewData = base.CreateViewData<ViewData<Category>, Category>(getArticle.Data);
            //viewData.Category = getArticle.Data;
            viewData.Canonical = getUrl.Data.Url;
            viewData.PageImage = getArticle.Data.PageImage;

            if (viewData.Category.Enabled == false) {
                viewData.NoIndex = true;
            }

            return View(viewData);
        }
    }
}