using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using _928.Core.ExceptionHandling;
using Google;
using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;
using Newtonsoft.Json;
using System.Web;

namespace _928.Core.UrlShortener {
    public class GoogleUrlShortenerService : IUrlShortenerService {
        private readonly UrlshortenerService service;
        private string apiKey;
        private Action<PolicyInfo> logAction;

        public GoogleUrlShortenerService(string appName, string apiKey, Action<PolicyInfo> logAction) {
            this.apiKey = apiKey;
            this.logAction = logAction;
            this.service = new UrlshortenerService(new BaseClientService.Initializer {
                ApiKey = apiKey,
                ApplicationName = appName
            });
        }
       
        public string CreateShortUrl(string urlToShorten) {

            try {
                var response = service.Url.Insert(new Url { LongUrl = urlToShorten }).Execute();
                return response.Id;
            } catch (Exception ex) {
#if DEBUGNOINTERNET
                return null;
#else
                var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                    new PolicyInfo {
                        OriginalException = typeof(Exception),
                        NewException = typeof(UrlShortnerException),
                        Message = "An error occurred while calling the URL Shortening service",
                        Behavior = ExceptionPolicyBehavior.Wrap
                    }));

                handler.HandleException(ex, logAction);
#endif
            }
            return string.Empty;
        }

        public UrlClicks GetClicks(string shortUrl) {

            try {

                if (shortUrl.Contains("goo.gl") == false)
                    shortUrl = "goo.gl/{0}".FormatWith(shortUrl);

                var request = service.Url.Get("http://{0}".FormatWith(shortUrl));
                request.Fields = "analytics,id,longUrl";
                request.Projection = UrlResource.GetRequest.ProjectionEnum.ANALYTICSCLICKS;

                var response = request.Execute();

                return new UrlClicks {
                    ShortUrl = response.Id,
                    LongUrl = response.LongUrl,
                    ShortUrlClicks = response.Analytics.AllTime.ShortUrlClicks,
                    LongUrlClicks = response.Analytics.AllTime.LongUrlClicks
                };

            } catch (Exception ex) {
#if DEBUGNOINTERNET
                return null;
#else
                var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                  new PolicyInfo {
                      OriginalException = typeof(Exception),
                      NewException = typeof(UrlShortnerException),
                      Message = "An error occurred while calling the URL Shortening service",
                      Behavior = ExceptionPolicyBehavior.Wrap
                  }));

                handler.HandleException(ex, logAction);
#endif
            }

            return null;

        }

        public ShortUrlAnalytics GetAnalyticsOld(string shortUrl) {
            try {

                if (shortUrl.Contains("goo.gl") == false)
                    shortUrl = "goo.gl/{0}".FormatWith(shortUrl);

                var request = service.Url.Get("http://{0}".FormatWith(shortUrl));
                request.PrettyPrint = false;
                request.Projection = UrlResource.GetRequest.ProjectionEnum.FULL;
                var response = request.Execute();

#if DEBUG
                var resultStream = request.ExecuteAsStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(resultStream);
                System.Diagnostics.Debug.Write(reader.ReadToEnd());
#endif
                var analytics = response.Analytics;

                var stats = new ShortUrlAnalytics();
                //{
                //    LongUrlClicks = analytics.AllTime.LongUrlClicks,
                //    ShortUrlClicks = analytics.AllTime.ShortUrlClicks
                //};

                //if (stats.ShortUrlClicks > 0) {
                //    stats.Browsers = analytics.AllTime.Browsers.Select(x => new StringCount { Id = x.Id, Count = x.Count }).ToList();
                //    stats.Countries = analytics.AllTime.Countries.Select(x => new StringCount { Id = x.Id, Count = x.Count }).ToList();
                //    stats.Platforms = analytics.AllTime.Platforms.Select(x => new StringCount { Id = x.Id, Count = x.Count }).ToList();
                //    stats.Referrers = analytics.AllTime.Referrers.Select(x => new StringCount { Id = x.Id, Count = x.Count }).ToList();
                //}

                return stats;

            } catch (Exception ex) {
#if DEBUGNOINTERNET
                return null;
#else
                var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                   new PolicyInfo {
                       OriginalException = typeof(Exception),
                       NewException = typeof(UrlShortnerException),
                       Message = "An error occurred while calling the URL Shortening service",
                       Behavior = ExceptionPolicyBehavior.Wrap
                   }));

                handler.HandleException(ex, logAction);
#endif
            }
            return null;
        }

        public ShortUrlAnalytics GetAnalytics(string shortUrl) {

            if (shortUrl.Contains("goo.gl") == false)
                shortUrl = "goo.gl/{0}".FormatWith(shortUrl);

            try {

                using (var wc = new WebClient()) {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    var data = new NameValueCollection();
                    data.Add("url", "http://{0}".FormatWith(shortUrl));
                    data.Add("security_token", this.apiKey);

                    var response = wc.UploadValues("https://goo.gl/api/analytics", "POST", data);

                    var resultString = Encoding.Default.GetString(response);

#if DEBUG
                    System.Diagnostics.Debug.WriteLine(resultString);
#endif

                    var url = JsonConvert.DeserializeObject<GooglUrl>(resultString);

                    var stats = new ShortUrlAnalytics {
                        ShortUrl = url.Id,
                        LongUrl = url.LongUrl,
                        PreviewUrl = url.PreviewUrl,
                        Created = url.Created,
                        AllTime = url.Analytics.AllTime,
                        Month = url.Analytics.Month,
                        Day = url.Analytics.Day,
                        Week = url.Analytics.Week,
                        TwoHours = url.Analytics.TwoHours
                    };

                    return stats;
                }

            } catch (Exception ex) {

#if DEBUGNOINTERNET
                return null;
#else
                var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                  new PolicyInfo {
                      OriginalException =typeof(Exception),
                      NewException = typeof(UrlShortnerException),
                      Message = "An error occurred while calling the URL Shortening service",
                      Behavior = ExceptionPolicyBehavior.Wrap
                  }));

                handler.HandleException(ex, logAction);
#endif
            }
            return null;
        }
    }
}
