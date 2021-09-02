using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    public class ShortUrl {

        public string Url {
            get {
                if (Key != null) {
                    return "{0}/{1}".FormatWith(ServiceDomain, Key);
                } else {
                    return string.Empty;
                }
            }
            set {
                if (value != null) {
                    this.ServiceDomain = value.Split('/')[2];
                    this.Key = value.Split('/')[3];
                }
            }
        }
        public string FullUrl {
            get {
                if (Key != null) {
                    return "http://{0}/{1}".FormatWith(ServiceDomain, Key);
                } else {
                    return string.Empty;
                }
            }
            set {
                if (value != null) {
                    this.ServiceDomain = value.Split('/')[2];
                    this.Key = value.Split('/')[3];
                }
            }
        }
        public string LongUrl { get; set; }
        public string ServiceDomain { get; set; }
        public string Key { get; set; }
    }
}
