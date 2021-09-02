using _928.Commands;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KyleFinley.Web.Controllers.Manage.Api
{
    public class VersionsController : ManagementApiController
    {
        public VersionsController(ICommandDispatcher dispatcher)
            : base(dispatcher) {

        }

        // GET: api/Api
        public IEnumerable<string> Get(EntityType entityType)
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Api/5
        public string Get(EntityType entityType, int id)
        {
            return "value";
        }

        // POST: api/Api
        public void Post(EntityType entityType, [FromBody]string value)
        {
        }

        // PUT: api/Api/5
        public void Put(EntityType entityType, int id, [FromBody]string value)
        {
        }

        // DELETE: api/Api/5
        public void Delete(EntityType entityType, int id)
        {
        }
    }
}
