using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.UrlShortener {
    internal class GooglUrl {
        [JsonProperty("details")]
        public virtual GooglAnalyticsSummary Analytics { get; set; }
        [JsonProperty("creation_time")]
        public virtual DateTime Created { get; set; }
        [JsonProperty("short_url")]
        public virtual string Id { get; set; }
        [JsonProperty("long_url")]
        public virtual string LongUrl { get; set; }
        [JsonProperty("status")]
        public virtual string Status { get; set; }
        [JsonProperty("preview_url")]
        public virtual string PreviewUrl { get; set; }
    }
}
