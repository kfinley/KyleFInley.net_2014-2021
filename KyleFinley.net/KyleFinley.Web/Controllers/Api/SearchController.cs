
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using _928.Commands;
using _928.Web.Mvc;

namespace KyleFinley.Web.Controllers.Api
{
    public class SearchController : BaseApiController
    {
        public SearchController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

    }
}
