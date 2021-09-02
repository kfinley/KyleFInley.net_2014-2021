using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    public class GooglAnalyticsSummary {
        
        [JsonProperty("all Time")]
        public virtual GooglAnalyticsSnapshot AllTime { get; set; }
        [JsonProperty("day")]
        public virtual GooglAnalyticsSnapshot Day { get; set; }
        public virtual string ETag { get; set; }
        [JsonProperty("month")]
        public virtual GooglAnalyticsSnapshot Month { get; set; }
        [JsonProperty("two hours")]
        public virtual GooglAnalyticsSnapshot TwoHours { get; set; }
        [JsonProperty("week")]
        public virtual GooglAnalyticsSnapshot Week { get; set; }
    }
}
