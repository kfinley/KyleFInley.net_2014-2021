using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    public class GooglAnalyticsSnapshot {
        
        [JsonProperty("clicks")]
        public virtual UrlClicks Clicks { get; set; }

        [JsonProperty("browsers")]
        public virtual IList<StringCount> Browsers { get; set; }
        [JsonProperty("countries")]
        public virtual IList<StringCount> Countries { get; set; }
        [JsonProperty("platforms")]
        public virtual IList<StringCount> Platforms { get; set; }
        [JsonProperty("referrers")]
        public virtual IList<StringCount> Referrers { get; set; }
    }
}
