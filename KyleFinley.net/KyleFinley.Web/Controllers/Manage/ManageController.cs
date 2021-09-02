using System.Web.Mvc;

using _928.Web.Mvc;
using _928.Commands;

using KyleFinley.Web.Models;

namespace KyleFinley.Web.Controllers.Manage {

    [Authorize]
    public class ManageController : ManagementController {

        public ManageController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        public ActionResult Index() {

            var viewData = this.CreateViewData<ManageViewData>();
            viewData.Canonical = Url.Action(this.Action());

            return View(viewData);
        }
    }
}