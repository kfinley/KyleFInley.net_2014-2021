using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class CreateArticle : BaseDataSourcedCommand<Article>, ICommand {

        public CreateArticle(IRepository<Article> repository, IHttpContext context)
            : base(repository, context) {
                this.UnitOfWork = repository.UnitOfWork;
        }

        public string Url { get; set; }
        public string Site { get; set; }

        public void Execute() {

            try {

                this.data.Author = "Kyle Finley";
                this.data.CreatedDate = DateTime.Now;
                this.data.LastModified = DateTime.Now;

                if (this.data.Enabled) {

                    var createTwitterShortUrl = CommandFactory.Create<CreateShortUrl>();
                    createTwitterShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.TwitterShare.FormatWith("Header-ShareThis-Button"));
                    dispatcher.Run(createTwitterShortUrl);

                    var createFacebookShortUrl = CommandFactory.Create<CreateShortUrl>();
                    createFacebookShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.FacebookShare.FormatWith("Header-ShareThis-Button"));
                    dispatcher.Run(createFacebookShortUrl);

                    var createGoogleShortUrl = CommandFactory.Create<CreateShortUrl>();
                    createGoogleShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.GoogleShare.FormatWith("Header-ShareThis-Button"));
                    dispatcher.Run(createGoogleShortUrl);

                    var createLinkedInShortUrl = CommandFactory.Create<CreateShortUrl>();
                    createLinkedInShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.LinkedInShare.FormatWith("Header-ShareThis-Button"));
                    dispatcher.Run(createLinkedInShortUrl);

                    var createEmailShortUrl = CommandFactory.Create<CreateShortUrl>();
                    createEmailShortUrl.LongUrl = "{0}/{1}?{2}".FormatWith(this.Site.ToLower(), this.Url, UtmParameters.EmailShare.FormatWith("Header-ShareThis-Button"));
                    dispatcher.Run(createEmailShortUrl);


                    this.data.TwitterShareUrl = createTwitterShortUrl.Data.Url;
                    this.data.FacebookShareUrl = createFacebookShortUrl.Data.Url;
                    this.data.GoogleShareUrl = createGoogleShortUrl.Data.Url;
                    this.data.LinkedInShareUrl = createLinkedInShortUrl.Data.Url;
                    this.data.EmailShareUrl = createEmailShortUrl.Data.Url;
                }

                repository.Insert(this.data);

                var createUrl = CommandFactory.Create<CreateUrl>();
                createUrl.Data = new SiteUrl {
                    EntityId = this.data.Id,
                    EntityType = (int)EntityType.Article,
                    Path = this.Url
                };
                createUrl.SharedContext(repository);

                dispatcher.Run(createUrl, false);

            } catch (Exception ex) {
                throw new Exception("Error creating Article. Message: {0}".FormatWith(ex.Message), ex);
            }
        }
    }
}
