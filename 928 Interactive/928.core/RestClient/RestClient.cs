using _928.Core.RestClient.Wiki;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _928.Core.RestClient {
    public class RestClient : IRestClient {
        public enum AcceptTypes {
            json,
            xml

        }
        private RestSharp.RestClient client;
        private readonly CookieContainer cookies = new CookieContainer();
        private RestRequest currentRequest;
        public RestClient() {
            client = new RestSharp.RestClient {
                CookieContainer = cookies,
                UserAgent = "MusicInTheRaw/0.1b (http://MusicInTheRaw.com; info@musicintheraw.com) using RestSharp/2.6'"  //TODO: This shouldn't be hardcoded!!
            };

        }

        public string BaseUrl {
            set {
                this.client.BaseUrl = new Uri(value);
            }
        }

        public string UserAgent {
            set {
                this.UserAgent = value;
            }
        }

        public void AddParamater(string name, object value) {
            this.AddParamater(name, value, RequestParameterType.QueryString);
        }

        public void AddParamater(string name, object value, RequestParameterType paramType) {
            if (currentRequest == null)
                currentRequest = new RestRequest();

            var existingParam = currentRequest.Parameters.Where(p => p.Name == name).FirstOrDefault();

            if (existingParam != null) {
                existingParam.Value = value;
            } else {
                currentRequest.AddParameter(name, value, (ParameterType)paramType);
            }
        }

        public void SetAcceptType(AcceptTypes acceptType) {
            if (currentRequest == null)
                currentRequest = new RestRequest();
            switch (acceptType) {
                case AcceptTypes.json:
                    break;
                case AcceptTypes.xml:
                    currentRequest.AddHeader("accept", "text/xml; charset=utf-8");
                    var deserializer = new RestSharp.Deserializers.XmlDeserializer();
                    deserializer.RootElement = currentRequest.RootElement;

                    client.AddHandler("text/xml; charset=utf-8", deserializer);
                    break;
                default:
                    break;
            }
        }
        public void AddHeader(string name, string value) {
            if (currentRequest == null)
                currentRequest = new RestRequest();

            currentRequest.AddHeader(name, value);

        }

        public void SetRootElement(string name) {
            if (currentRequest == null)
                currentRequest = new RestRequest();

            currentRequest.RootElement = name;

        }
        public string Execute() {
            var response = client.Execute(currentRequest);
            this.currentRequest = null;

            if (response.ErrorException != null) {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var restClientException = new ApplicationException(message, response.ErrorException);
                throw restClientException;
            }

            return response.Content;
        }

        public T Execute<T>() where T : new() {
            //var response = client.ExecuteAsync<T>(currentRequest);
            var response = client.Execute<T>(currentRequest);

            switch (response.ContentType) {
                case "text/xml; charset=utf-8":
                    switch (typeof(T).ToString()) {
                        case "_928.Core.RestClient.Wiki.WikiData": // TODO: This sucks. Get the xml deserialization working!
                            T data = Activator.CreateInstance<T>();
                            var doc = XDocument.Parse(response.Content);

                            data.ToType<WikiData>(new WikiData()).PageId = doc.Root.Descendants("page").Select(p => p).First().Attributes("pageid").First().Value; //.Attributes["pageid"].Value;
                            data.ToType<WikiData>(new WikiData()).Title = doc.Root.Descendants("page").Select(p => p).First().Attributes("title").First().Value;
                            data.ToType<WikiData>(new WikiData()).WikiText = doc.Root.Descendants("rev").Select(p => p).First().Value;

                            doc = XDocument.Parse(doc.Root.Descendants("rev").Select(p => p).First().Attributes("parsetree").First().Value);
                           
                            //data.ToType<WikiData>(new WikiData()).Title = doc.SelectNodes("//page")[0].Attributes["title"].Value;
                            //doc.LoadXml(doc.SelectNodes("//rev")[0].Attributes["parsetree"].Value);

                            data.ToType<WikiData>(new WikiData()).Document = doc;

                            return data;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            //    response.Headers.Select(h => h.n)
            this.currentRequest = null;

            if (response.ErrorException != null) {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var restClientException = new ApplicationException(message, response.ErrorException);
                throw restClientException;
            }
            return response.Data;
        }

    }

    public enum RequestParameterType {
        Cookie = 0,
        GetOrPost = 1,
        UrlSegment = 2,
        HttpHeader = 3,
        RequestBody = 4,
        QueryString = 5,
    }
}
