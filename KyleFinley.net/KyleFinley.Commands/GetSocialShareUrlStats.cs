using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Entities.Models;

namespace KyleFinley.Commands {
    public class GetSocialSharesUrlStats : BaseCommand<Page>, ICommand {

        public GetSocialSharesUrlStats(IHttpContext context) 
            : base(context) {
        }

        public void Execute() {
            try {
                var getTwitterShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getTwitterShareUrlStats.ShortUrl = this.Data.TwitterShareUrl;

                dispatcher.Run(getTwitterShareUrlStats);

                var getGoogleShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getGoogleShareUrlStats.ShortUrl = this.Data.GoogleShareUrl;

                dispatcher.Run(getGoogleShareUrlStats);

                var getFacebookShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getFacebookShareUrlStats.ShortUrl = this.Data.FacebookShareUrl;

                dispatcher.Run(getFacebookShareUrlStats);

                var getLinkedIndShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getLinkedIndShareUrlStats.ShortUrl = this.Data.LinkedInShareUrl;

                dispatcher.Run(getLinkedIndShareUrlStats);

                var getEmailShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getEmailShareUrlStats.ShortUrl = this.Data.EmailShareUrl;

                var getPinterestShareUrlStats = CommandFactory.Create<GetShortUrlClicks>();
                getPinterestShareUrlStats.ShortUrl = this.Data.PinterestShareUrl;

                dispatcher.Run(getEmailShareUrlStats);

                this.Data.TwitterShareUrlClicks = getTwitterShareUrlStats.Data;
                this.Data.GoogleShareUrlClicks = getGoogleShareUrlStats.Data;
                this.Data.FacebookShareUrlClicks = getFacebookShareUrlStats.Data;
                this.Data.LinkedInShareUrlClicks = getLinkedIndShareUrlStats.Data;
                this.Data.EmailShareUrlClicks = getEmailShareUrlStats.Data;

            } catch (Exception ex) {
                throw new Exception("Error in GetSocialSharesUrlStats. Message: {0}".FormatWith(ex.Message), ex);
            }
        }

    }
}
