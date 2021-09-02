using _928.Commands;
using _928.Core;
using _928.Core.Interfaces;
using _928.UrlShortener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class GetShortUrlAnalytics : BaseCommand<ShortUrlAnalytics>, ICommand {

         private readonly IUrlShortenerService service;

         public GetShortUrlAnalytics(IUrlShortenerService service, IHttpContext context) 
         : base(context) {
            this.service = service;
        }

        public string ShortUrl { get; set; }
        public string ShortUrlKey { get; set; }

        public void Execute() {

            if (this.ShortUrl.HasValue()) {
                this.data = service.GetAnalytics(this.ShortUrl);
            
            } else if (this.ShortUrlKey.HasValue()) {
                this.data = service.GetAnalytics(this.ShortUrlKey);
            }
            
        }
    }
}
