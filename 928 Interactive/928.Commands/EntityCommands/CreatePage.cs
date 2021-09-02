using _928.Commands;
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
    public class CreatePage<T> : BaseDataSourcedCommand<Page<T>, Page>, ICommand
        where T : Entity {

        private IRepository<T> entityRepository;

        public CreatePage(IRepository<Page> repository, IRepository<T> entityRepository, IHttpContext context)
            : base(repository, context) {
            this.entityRepository = entityRepository;

            this.entityRepository.UnitOfWork = repository.UnitOfWork;
            this.UnitOfWork = repository.UnitOfWork;
        }

        public string Url { get; set; }
        public string Site { get; set; }

        public void Execute() {

            try {

                this.data.CreatedDate = DateTime.Now;
                this.data.LastModified = DateTime.Now;
                
                this.data.Entity.Name = this.data.Title;
                this.data.Entity.CreatedDate = DateTime.Now;
                this.data.Entity.LastModified = DateTime.Now;
                
                var entity = entityRepository.Insert(this.data.Entity);

                if (this.data.Enabled) {

                    var createShortUrls = CommandFactory.Create<CreatePageShortUrls>();
                    createShortUrls.Site = this.Site;
                    createShortUrls.Url = this.Url;
                    createShortUrls.Data = this.data;
                    dispatcher.Run(createShortUrls, false);

                   this.data.PublishedDate = DateTime.Now;
                   
                }

                var page = this.data.MapTo(new Page());
                page.Path = this.Url;
                page.EntityType = entity.EntityType;
                page.EntityId = entity.Id;
                
                repository.Insert(page);

                page.MapTo(this.data);
                this.data.Entity = entity;

            } catch (Exception ex) {
                throw new Exception("Error creating Article. Message: {0}".FormatWith(ex.Message), ex);
            }
        }
    }
}
