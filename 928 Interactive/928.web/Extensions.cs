using _928.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//using System.Web.WebPages.Html;
using System.IO;

namespace _928.Web
{
    public static class Extensions
    {
        #region json
        public static string ToJson<T>(this IList<T> list)
        {
            var oSerializer = new JavaScriptSerializer();
            string json = oSerializer.Serialize(list);

            return json;
        }
        #endregion

        public static AppMode ApplicationMode(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return AppMode.Development;
#else
            return AppMode.Production;
#endif

        }

        public static AppMode ApplicationMode(this HttpApplicationStateBase application)
        {
#if DEBUG
            return AppMode.Development;
#else
            return AppMode.Production;
#endif
        }

        public static AppMode ApplicationMode(this HtmlHelper<dynamic> htmlHelper)
        {
#if DEBUG
            return AppMode.Development;
#else
            return AppMode.Production;
#endif
        }
    }

    public static class HttpRequestExtensions
    {

        /// <summary>
        /// Dump the raw http request to a string. 
        /// </summary>
        /// <param name="request">The <see cref="HttpRequest"/> that should be dumped.       </param>
        /// <returns>The raw HTTP request.</returns>
        public static string ToRaw(this HttpRequestBase request)
        {
            StringWriter writer = new StringWriter();

            WriteStartLine(request, writer);
            WriteHeaders(request, writer);
            WriteBody(request, writer);


            return writer.ToString();
        }

        private static void WriteStartLine(HttpRequestBase request, StringWriter writer)
        {
            const string SPACE = " ";

            writer.Write(request.HttpMethod);
            writer.Write(SPACE + request.Url);
            writer.WriteLine("{0}IP: {1}".FormatWith(SPACE, request.UserHostAddress));
            writer.WriteLine("{0}Hostname: {1}".FormatWith(SPACE, request.UserHostName));
            writer.WriteLine("{0}Referrer: {1}".FormatWith(SPACE, request.UrlReferrer));

            writer.WriteLine(SPACE + request.ServerVariables["SERVER_PROTOCOL"]);
        }

        private static void WriteHeaders(HttpRequestBase request, StringWriter writer)
        {
            foreach (string key in request.Headers.AllKeys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, request.Headers[key]));
            }

            writer.WriteLine();
        }

        private static void WriteBody(HttpRequestBase request, StringWriter writer)
        {
            StreamReader reader = new StreamReader(request.InputStream);

            try
            {
                string body = reader.ReadToEnd();
                writer.WriteLine(body);
            }
            finally
            {
                reader.BaseStream.Position = 0;
            }
        }

        public static string ToRaw(this HttpRequest request)
        {
            StringWriter writer = new StringWriter();

            WriteStartLine(request, writer);
            WriteHeaders(request, writer);
            WriteBody(request, writer);


            return writer.ToString();
        }

        private static void WriteStartLine(HttpRequest request, StringWriter writer)
        {
            const string SPACE = " ";

            writer.Write(request.HttpMethod);
            writer.Write(SPACE + request.Url);
            writer.WriteLine("{0}IP: {1}".FormatWith(SPACE, request.UserHostAddress));
            writer.WriteLine("{0}Hostname: {1}".FormatWith(SPACE, request.UserHostName));
            writer.WriteLine("{0}Referrer: {1}".FormatWith(SPACE, request.UrlReferrer));

            writer.WriteLine(SPACE + request.ServerVariables["SERVER_PROTOCOL"]);
        }

        private static void WriteHeaders(HttpRequest request, StringWriter writer)
        {
            foreach (string key in request.Headers.AllKeys)
            {
                writer.WriteLine(string.Format("{0}: {1}", key, request.Headers[key]));
            }

            writer.WriteLine();
        }

        private static void WriteBody(HttpRequest request, StringWriter writer)
        {
            StreamReader reader = new StreamReader(request.InputStream);

            try
            {
                string body = reader.ReadToEnd();
                writer.WriteLine(body);
            }
            finally
            {
                reader.BaseStream.Position = 0;
            }
        }
    }
}