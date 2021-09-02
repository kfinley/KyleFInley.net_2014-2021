using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using _928.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class EditPage<T> : BaseDataSourcedCommand<Page<T>, Page>, ICommand
         where T : Entity {

        private IRepository<T> entityRepository;

        public EditPage(IRepository<Page> repository, IRepository<T> entityRepository, IHttpContext httpContext)
            : base(repository, httpContext) {
                this.entityRepository = entityRepository;
                this.SharedContext(entityRepository);
        }

        public void Execute() {

            try {

                this.data.LastModified = DateTime.Now;
                if (this.data.PublishedDate.IsNull() && this.data.Enabled == true) {
                    this.data.PublishedDate = DateTime.Now;
                }

                if (this.data.Enabled) {
                    var createShortUrls = CommandFactory.Create<CreatePageShortUrls>();
                    createShortUrls.Site = this.Site;
                    createShortUrls.Url = this.Url;
                    createShortUrls.Data = this.data;
                    dispatcher.Run(createShortUrls, false);
                }

                var page = this.data.MapTo(new Page());
                
                repository.Update(page);
                entityRepository.Update(this.data.Entity);

                repository.UnitOfWork.Commit();

                page.MapTo(this.data);

                // update page in cache
                this.HttpContext.Cache[Id.ToString()] = this.data;

            } catch (Exception ex) {

                throw new Exception("Error updating {0} Message: {1}".FormatWith(this.data != null ? this.data.GetType().Name : "Entity", ex.Message), ex);
            }
        }

        public string Site { get; set; }
        public string Url { get; set; }
    }
}
