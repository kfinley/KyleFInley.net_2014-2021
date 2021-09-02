using System.Web.Mvc;
using System.IO;
using System.Web;

namespace _928.Web.Mvc.Results {
    public class CachedFileResult : ActionResult {


        public CachedFileResult(string filePath, string fileName, string contentType) {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context) {

            byte[] fileBytes;

            if (HttpContext.Current.Cache[this.FilePath] == null) {
                HttpContext.Current.Cache[this.FilePath] = File.ReadAllBytes(this.FilePath);
            }

            fileBytes = HttpContext.Current.Cache[this.FilePath] as byte[];

            var response = HttpContext.Current.Response;
            response.Buffer = true;
            response.Charset = "";

            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = this.ContentType;

            response.AddHeader("content-disposition", "attachment;filename=" + this.FileName);

            response.BinaryWrite(fileBytes);
            response.Flush();
            response.End();
        }


        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
