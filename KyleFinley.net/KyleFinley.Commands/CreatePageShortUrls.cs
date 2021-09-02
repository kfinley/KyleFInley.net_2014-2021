using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Core;

using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _928.Entities.Models;

namespace KyleFinley.Commands {
    public class CreatePageShortUrls  : BaseCommand<Page>, ICommand {

        public CreatePageShortUrls(IHttpContext context)
            : base(context) {
        }

        public void Execute() {
            
            if (this.data.TwitterShareUrl.IsEmpty()) {

                var createTwitterShortUrl = CommandFactory.Create<CreateShortUrl>();
                createTwitterShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.TwitterShare.FormatWith("Header-ShareThis-Button"));
                dispatcher.Run(createTwitterShortUrl);

                this.data.TwitterShareUrl = createTwitterShortUrl.Data.Url;
            }


            if (this.data.FacebookShareUrl.IsEmpty()) {

                var createFacebookShortUrl = CommandFactory.Create<CreateShortUrl>();
                createFacebookShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.FacebookShare.FormatWith("Header-ShareThis-Button"));
                dispatcher.Run(createFacebookShortUrl);

                this.data.FacebookShareUrl = createFacebookShortUrl.Data.Url;
            }

            if (this.data.GoogleShareUrl.IsEmpty()) {

                var createGoogleShortUrl = CommandFactory.Create<CreateShortUrl>();
                createGoogleShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.GoogleShare.FormatWith("Header-ShareThis-Button"));
                dispatcher.Run(createGoogleShortUrl);

                this.data.GoogleShareUrl = createGoogleShortUrl.Data.Url;
            }

            if (this.data.LinkedInShareUrl.IsEmpty()) {

                var createLinkedInShortUrl = CommandFactory.Create<CreateShortUrl>();
                createLinkedInShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.LinkedInShare.FormatWith("Header-ShareThis-Button"));
                dispatcher.Run(createLinkedInShortUrl);

                this.data.LinkedInShareUrl = createLinkedInShortUrl.Data.Url;
            }

            if (this.data.EmailShareUrl.IsEmpty()) {

                var createEmailShortUrl = CommandFactory.Create<CreateShortUrl>();
                createEmailShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.EmailShare.FormatWith("Header-ShareThis-Button"));
                dispatcher.Run(createEmailShortUrl);

                this.data.EmailShareUrl = createEmailShortUrl.Data.Url;

            }
        }
        public string Site { get; set; }
        public string Url { get; set; }
    }
}
