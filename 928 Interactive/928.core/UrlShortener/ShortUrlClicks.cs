using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    public class UrlClicks {
        public string ShortUrl { get; set; }
        public string LongUrl { get; set; }
        [JsonProperty("short_url")]
        public virtual long? ShortUrlClicks { get; set; }
        [JsonProperty("long_url")]
        public virtual long? LongUrlClicks { get; set; }
        [JsonProperty("buckets")]
        public virtual IList<int> Buckets { get; set; }
        //[JsonProperty("end_time", ItemConverterType = typeof(JavaScriptDateTimeConverter))]
        //public virtual DateTime EndTime { get; set; }
        [JsonProperty("end_time")]
        public virtual int EndTime { get; set; }
        [JsonProperty("bucket_size")]
        public virtual int BucketSize { get; set; }
    }
}
