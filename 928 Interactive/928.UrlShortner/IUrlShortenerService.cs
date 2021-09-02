using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core.Interfaces;

namespace _928.UrlShortener {
    public interface IUrlShortenerService {
        string CreateShortUrl(string urlToShorten);
        ShortUrlAnalytics GetAnalytics(string shortUrl);
        IUrlClicks GetClicks(string shortUrl);
    }
}
