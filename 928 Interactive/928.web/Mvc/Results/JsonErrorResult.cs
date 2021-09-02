using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _928.Web.Mvc.Results
{
    public class JsonErrorResult : JsonResult
    {
        public JsonErrorResult() : base()
        {

            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            Status = HttpStatusCode.BadRequest;
        }
        public HttpStatusCode Status
        {
            set
            {
                HttpContext.Current.Response.StatusCode = (int)value;
            }
        }
    }
}
