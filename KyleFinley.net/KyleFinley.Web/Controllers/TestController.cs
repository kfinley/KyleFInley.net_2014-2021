using _928.Commands;
using KyleFinley.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KyleFinley.Web.Controllers
{
    public class TestController : SiteController
    {
        public TestController(ICommandDispatcher dispatcher) 
            : base(dispatcher){

        }
        // GET: Test
        public ActionResult Index()
        {
            return View(new SiteViewData());
        }
    }
}