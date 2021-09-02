using System.Net;
using System.Web;
using System.Web.Mvc;

namespace _928.Web.Mvc.Results
{
    public class JsonRedirectResult : JsonResult
    {
        public JsonRedirectResult() : base()
        {

            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            Status = HttpStatusCode.Redirect;
        }

        public string Redirect
        {
            set
            {
                this.Data = new { Redirect = value };
            }
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
