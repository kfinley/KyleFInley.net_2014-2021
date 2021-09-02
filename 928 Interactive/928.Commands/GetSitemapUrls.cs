using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;

using KyleFinley.Models;

namespace KyleFinley.Commands {
    
    public class GetSitemapUrls : BaseDataSourcedCommand<IList<SiteUrl>, SiteUrl>, ICommand {

        public GetSitemapUrls(IRepository<SiteUrl> repository, IHttpContext context)
            : base(repository, context) {
        }
        

        public void Execute() {

            var getArticles = CommandFactory.Create<GetPages<Article>>();
            getArticles.RetrieveEnabledOnly = true;
            getArticles.SharedContext(repository);

            this.data = repository.All()
                                .Join(getArticles.Query(), u => u.EntityId, a => a.Id, (u, a) => new {
                                    Id = u.Id,
                                    Url = u.Path,
                                    LastModified = a.LastModified,
                                    PublishedDate = a.PublishedDate
                                }).OrderByDescending(a => a.PublishedDate)
            .AsEnumerable()
            .Select(url => new SiteUrl {
                Id = url.Id,
                Path = url.Url,
                LastModified = url.LastModified
            }).ToList();

            // Add a URL for the root
            this.data.Insert(0, new SiteUrl {
                Path = ""
            });
        }
    }
}
