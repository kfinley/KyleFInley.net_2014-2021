
using _928.Core.Interfaces;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.UrlShortener {
    public class ShortUrlAnalytics : Entity, IEntity  {

        public virtual string ShortUrl { get; set; }
        public virtual string LongUrl { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string PreviewUrl { get; set; }

        public virtual GooglAnalyticsSnapshot AllTime { get; set; }
        public virtual GooglAnalyticsSnapshot Month { get; set; }
        public virtual GooglAnalyticsSnapshot Week { get; set; }
        public virtual GooglAnalyticsSnapshot Day { get; set; }
        public virtual GooglAnalyticsSnapshot TwoHours { get; set; }
    }

    public class StringCount {
        public virtual string Id { get; set; }
        public virtual long? Count { get; set; }
    }
}
