using System;
namespace _928.UrlShortner {
    interface IUrlClicks {
        global::System.Collections.Generic.IList<int> Buckets { get; set; }
        int BucketSize { get; set; }
        int EndTime { get; set; }
        string LongUrl { get; set; }
        long? LongUrlClicks { get; set; }
        string ShortUrl { get; set; }
        long? ShortUrlClicks { get; set; }
    }
}
