using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.IO;
using _928.Core;
using System.Web.Mvc;

namespace _928.Web.Mvc.Security {
    public class SecureImageHttpHandler : MvcHandler, IHttpHandler, IHttpAsyncHandler {

        public SecureImageHttpHandler(RequestContext requestContext)
            : base(requestContext) {
        }

        void IHttpHandler.ProcessRequest(HttpContext context) {

            HandleRouting();
            base.ProcessRequest(context);
        }

        System.IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, System.AsyncCallback cb, object extraData) {
            HandleRouting();
            return base.BeginProcessRequest(context, cb, extraData);
        }

        void IHttpAsyncHandler.EndProcessRequest(System.IAsyncResult result) {
            base.EndProcessRequest(result);
        }

        bool IHttpHandler.IsReusable {
            get { return false; }
        }

        private void HandleRouting() {


            var pageId = RequestContext.RouteData.Values["pageId"].ToString();
            var itemIdentifier = RequestContext.RouteData.Values["itemIdentifier"].ToString();
            var subdirectory = RequestContext.RouteData.Values["subdirectory"];
            var AppKeyItemId = "{0}|{1}".FormatWith(pageId, itemIdentifier);

            RequestContext.RouteData.Values["controller"] = "Content";
            RequestContext.RouteData.Values["action"] = "SecureImage";

            RequestContext.RouteData.Values["appKeyItemId"] = "{0}|{1}".FormatWith(pageId, itemIdentifier);
            RequestContext.RouteData.Values["itemIdentifier"] = itemIdentifier;
            RequestContext.RouteData.Values["subdirectory"] = subdirectory;

            //var response = requestContext.HttpContext.Response;
            //var request = requestContext.HttpContext.Request;
            //var server = requestContext.HttpContext.Server;

            //var pageId = requestContext.RouteData.Values["pageId"].ToString();
            //var sequenceNumber = requestContext.RouteData.Values["itemIdentifier"].ToString();

            //var subdirectory = requestContext.RouteData.Values["subdirectory"];

            //var serverPath = "~/content/images/members/galleries";
            
            //var path = server.MapPath(serverPath);

            //response.Clear();
            //response.ContentType = GetContentType(request.Url.ToString());

            //var pageInfo = requestContext.HttpContext.Application[pageId];

            //if (pageInfo == null) {
            //    response.Write("We do not allow that. You must view our images on the website. Your IP Address and user information has been recorded by our system administrators.");
            //} else {
            //    requestContext.HttpContext.Application.Remove(pageId);
            //    if (subdirectory != null) {
            //        var filePath = "{0}\\{1}\\{2}\\{3}.jpg".FormatWith(path, pageInfo, subdirectory.ToString(), sequenceNumber);
            //        response.TransmitFile(filePath);
            //    } else {
            //        var filePath = "{0}\\{1}\\{2}.jpg".FormatWith(path, pageInfo, sequenceNumber);
            //        response.TransmitFile(filePath);
            //    }
            //}

            //response.End();

            //const string invalidRequestFile = "thief.gif";

            //if (request.ServerVariables["HTTP_REFERER"] != null &&
            //    request.ServerVariables["HTTP_REFERER"].Contains("studio969.net")) {
            //    response.TransmitFile("{0}/{1}/{2}", pageInfo.Item2+ validRequestFile);
            //} else {
            //    response.TransmitFile(path + invalidRequestFile);
            //}
            //var vanityUrl = RequestContext.RouteData.Values["vanityUrl"].ToString();

            //var getVanityUrl = CommandFactory.Create(UrlHandlerCommandType, new Tuple<string, object>("vanityUrl", vanityUrl));

            //getVanityUrl.Execute();

        }

        //public Type UrlHandlerCommandType { get; set; }

    }
}