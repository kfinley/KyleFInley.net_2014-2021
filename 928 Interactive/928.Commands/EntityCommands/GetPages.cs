
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Core.Linq;
using _928.Data.Repository;
using _928.Entities.Models;
using System.Data.SqlClient;

namespace _928.Commands {

    public class GetPages<T> : BaseDataSourcedCommand<IList<Page<T>>, Page>, ICommand 
        where T : IEntity {

        private IOrderByExpression<Page<T>> orderBy = new OrderByExpression<Page<T>, DateTime>(e => e.PublishedDate.Value, SortOrder.Descending);

        public GetPages(IRepository<Page> repository, IHttpContext context)
            : base(repository, context) {
        }

        public bool RetrieveEnabledOnly { get; set; }

        public int EntityType { get; set; }
        
        public IQueryableCommand EntityCommand { get; set; }

        public void OrderBy<TKeyType>(OrderByExpression<Page<T>, TKeyType> expression) {
            this.orderBy = expression;
        }

        public IQueryable<Page<T>> Query() {

            try {
                EntityCommand.SharedContext(repository);

                if (this.RetrieveEnabledOnly) {

                    return orderBy.ApplyOrdering(repository.All()
                                                .Join(EntityCommand.Query(), p => p.EntityId, e => e.Id, (p, e) => new {
                                                    Id = p.Id,
                                                    EntityType = p.EntityType,
                                                    EntityId = e.Id,
                                                    Title = p.Title,
                                                    Description = p.Description,
                                                    Content = p.Content,
                                                    PublishedDate = p.PublishedDate,
                                                    Path = p.Path,
                                                    Enabled = p.Enabled,
                                                    Entity = e,
                                                    CreateDate = p.CreatedDate,
                                                    LastModified = p.LastModified,
                                                    EmailShareUrl = p.EmailShareUrl,
                                                    FacebookShareUrl = p.FacebookShareUrl,
                                                    PageImage = p.PageImage,
                                                    PinterestShareUrl = p.PinterestShareUrl,
                                                    TwitterShareUrl = p.TwitterShareUrl,
                                                    LinkedInShareUrl = p.LinkedInShareUrl,
                                                    GoogleShareUrl = p.GoogleShareUrl
                                                }).Where(p => p.Enabled == true && (this.EntityType > -1) ? p.EntityType == this.EntityType : true)
                                                .Select(page => new Page<T> {
                                                    Id = page.Id,
                                                    EntityType = page.EntityType,
                                                    EntityId = page.EntityId,
                                                    Entity = (T)page.Entity,
                                                    Title = page.Title,
                                                    Path = page.Path,
                                                    Enabled = page.Enabled,
                                                    Content = page.Content,
                                                    CreatedDate = page.CreateDate,
                                                    PublishedDate = page.PublishedDate,
                                                    Description = page.Description,
                                                    LastModified = page.LastModified,
                                                    PageImage = page.PageImage,
                                                    EmailShareUrl = page.EmailShareUrl,
                                                    FacebookShareUrl = page.FacebookShareUrl,
                                                    PinterestShareUrl = page.PinterestShareUrl,
                                                    TwitterShareUrl = page.TwitterShareUrl,
                                                    LinkedInShareUrl = page.LinkedInShareUrl,
                                                    GoogleShareUrl = page.GoogleShareUrl
                                                }));

                    //var pages = (from p in repository.All()
                    //        where p.Enabled == true
                    //        select p).ToList();


                    //foreach (var page in pages) {
                    //    this.data.Add(page.MapTo(new Page<T>()));
                    //}

                    //return this.data.AsQueryable();

                    //return orderBy.ApplyOrdering(from p in repository.All()
                    //                             where p.Enabled == true
                    //                             && (this.EntityType > -1) ? p.EntityType == this.EntityType : true
                    //                             select p);
                } else {

                    //return orderBy.ApplyOrdering(from p in repository.All()
                    //                             where (this.EntityType > -1) ? p.EntityType == this.EntityType : true
                    //                             select p);

                    return orderBy.ApplyOrdering(repository.All()
                                                   .Join(EntityCommand.Query(), p => p.EntityId, e => e.Id, (p, e) => new {
                                                       Id = p.Id,
                                                       EntityType = p.EntityType,
                                                       EntityId = e.Id,
                                                       Title = p.Title,
                                                       Description = p.Description,
                                                       Content = p.Content,
                                                       PublishedDate = p.PublishedDate,
                                                       Path = p.Path,
                                                       Enabled = p.Enabled,
                                                       Entity = e,
                                                       CreateDate = p.CreatedDate,
                                                       LastModified = p.LastModified,
                                                       EmailShareUrl = p.EmailShareUrl,
                                                       FacebookShareUrl = p.FacebookShareUrl,
                                                       PageImage = p.PageImage,
                                                       PinterestShareUrl = p.PinterestShareUrl,
                                                       TwitterShareUrl = p.TwitterShareUrl,
                                                       LinkedInShareUrl = p.LinkedInShareUrl,
                                                       GoogleShareUrl = p.GoogleShareUrl
                                                   }).Where(p => (this.EntityType > -1) ? p.EntityType == this.EntityType : true)
                                                   .Select(page => new Page<T> {
                                                       Id = page.Id,
                                                       EntityType = page.EntityType,
                                                       EntityId = page.EntityId,
                                                       Entity = (T)page.Entity,
                                                       Title = page.Title,
                                                       Path = page.Path,
                                                       Enabled = page.Enabled,
                                                       Content = page.Content,
                                                       CreatedDate = page.CreateDate,
                                                       PublishedDate = page.PublishedDate,
                                                       Description = page.Description,
                                                       LastModified = page.LastModified,
                                                       PageImage = page.PageImage,
                                                       EmailShareUrl = page.EmailShareUrl,
                                                       FacebookShareUrl = page.FacebookShareUrl,
                                                       PinterestShareUrl = page.PinterestShareUrl,
                                                       TwitterShareUrl = page.TwitterShareUrl,
                                                       LinkedInShareUrl = page.LinkedInShareUrl,
                                                       GoogleShareUrl = page.GoogleShareUrl
                                                   }));
                }
            } catch (NullReferenceException nre) {
                if (EntityCommand == null) {
                    throw new Exception("EntityCommand is null!");
                }
                throw nre;
            }
         
        }

        public void Execute() {

            try {

                //if (this.RetrieveEnabledOnly) {

                //    var pages = (from p in repository.All()
                //            where p.Enabled == true
                //            select p).ToList();


                //    foreach (var page in pages) {
                //        this.data.Add(page.MapTo(new Page<T>()));
                //    }


                //}

                this.data = this.Query().ToList();

            } catch (Exception ex) {
                throw new Exception("Error retrieving list.", ex);
            }
        }
    }
}
