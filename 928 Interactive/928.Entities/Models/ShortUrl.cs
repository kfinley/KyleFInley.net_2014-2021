using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using _928.Core;
using _928.Core.Interfaces;

namespace _928.Entities.Models {
    public class ShortUrl : IShortUrl {

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
