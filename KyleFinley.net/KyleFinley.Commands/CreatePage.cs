using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities.Models;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class CreatePage<T> : BaseDataSourcedCommand<T>, ICommand
        where T : Page {

        public CreatePage(IRepository<T> repository, IHttpContext context)
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

                    var createShortUrls = CommandFactory.Create<CreatePageShortUrls>();
                    createShortUrls.Site = this.Site;
                    createShortUrls.Url = this.Url;
                    createShortUrls.Data = this.data;
                    dispatcher.Run(createShortUrls, false);

                }

                repository.Insert(this.data);

                var createUrl = CommandFactory.Create<CreateUrl>();
                createUrl.Data = new SiteUrl {
                    EntityId = this.data.Id,
                    EntityType = this.data.EntityType,
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
