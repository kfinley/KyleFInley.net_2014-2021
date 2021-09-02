using System;
namespace _928.Entities.Models {
    interface IShortUrl {
        string FullUrl { get; set; }
        string Key { get; set; }
        string LongUrl { get; set; }
        string ServiceDomain { get; set; }
        string Url { get; set; }
    }
}
