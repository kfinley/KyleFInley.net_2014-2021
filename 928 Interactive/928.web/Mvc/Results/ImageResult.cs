
using System.Web.Mvc;
using System.IO;

namespace _928.Web.Mvc.Results {
    public class ImageResult : FileStreamResult {
        // the base class already assigns the 401.  
        // we bring these constructors with us to allow setting status text
        public ImageResult(Stream fileStream, string contentType) : base(fileStream, contentType) { }

        public override void ExecuteResult(ControllerContext context) {

            base.ExecuteResult(context);
        }
    }
}