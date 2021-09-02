using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    public interface IUrlShortenerService {
        string CreateShortUrl(string urlToShorten);
        ShortUrlAnalytics GetAnalytics(string shortUrl);
        UrlClicks GetClicks(string shortUrl);
    }
}
