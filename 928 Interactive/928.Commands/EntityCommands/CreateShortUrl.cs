using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;

using _928.UrlShortener;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Core.ExceptionHandling;
using _928.Entities.Models;

namespace _928.Commands {
    public class CreateShortUrl : BaseCommand<ShortUrl>, ICommand {

        private readonly IUrlShortenerService service;

        public CreateShortUrl(IUrlShortenerService service, IHttpContext context)
            : base(context) {
            this.service = service;
        }

        public string LongUrl { get; set; }

        public void Execute() {

            try {
                var shortUrl = service.CreateShortUrl(this.LongUrl);

                this.data = new ShortUrl {
                    LongUrl = this.LongUrl,
                    Url = shortUrl != null ? shortUrl : null
                };
            } catch (Exception ex) {

                throw new Exception("Error creating Short Url for {0}. Message: {1}".FormatWith(this.LongUrl, ex.Message), ex);
            }
        }
    }
}
