using System;
using System.Collections.Generic;

namespace _928.Core.Interfaces {
    public interface IUrlClicks {
        IList<int> Buckets { get; set; }
        int BucketSize { get; set; }
        int EndTime { get; set; }
        string LongUrl { get; set; }
        long? LongUrlClicks { get; set; }
        string ShortUrl { get; set; }
        long? ShortUrlClicks { get; set; }
    }
}
