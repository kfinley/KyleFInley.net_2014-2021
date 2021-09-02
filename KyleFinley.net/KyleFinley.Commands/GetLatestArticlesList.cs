using _928.Core;
using _928.Core.Interfaces;
using _928.Commands;
using _928.Data;
using _928.Core.Linq;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _928.Data.Repository;
using _928.Entities;
using _928.Entities.Models;

namespace KyleFinley.Commands {
    public class GetLatestArticlesList : BaseDataSourcedCommand<IList<ArticleSummary>, Article>, ICommand {

        private IOrderByExpression<Page> orderBy = new OrderByExpression<Page, DateTime>(a => a.PublishedDate.Value, System.Data.SqlClient.SortOrder.Descending);

        public GetLatestArticlesList(IRepository<Article> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            var latestArticles = context.Cache["LatestArticles"] as IList<ArticleSummary>;

            if (latestArticles.IsNull()) {

                var getPages = CommandFactory.Create<GetPages<Article>>();
                getPages.EntityType = (int)EntityType.Article;
                getPages.SharedContext(repository); 
                getPages.EntityCommand = CommandFactory.Create<GetArticles>();

                this.data = repository.All()
                  .Join(getPages.Query(), a => a.Id, p => p.EntityId, (a, p) => new {
                      Id = a.Id,
                      Title = p.Title,
                      Description = p.Description,
                      Content = p.Content,
                      PublishedDate = p.PublishedDate,
                      UrlId = p.Id,
                      UrlPath = p.Path,
                      UrlEntityType = p.EntityType,
                      UrlEntityId = p.EntityId,
                      Enabled = p.Enabled,
                      Entity = p.Entity,
                      Author = p.Entity.Author
                  }).Where(page => /*a.PublishedDate > new DateTime(2014, 01, 01) &&*/ page.Enabled == true)
                  .OrderByDescending(a => a.PublishedDate)
                  .Take(5)
                  .AsEnumerable()
                  .Select(article => new ArticleSummary {
                      Id = article.Id,
                      Author = article.Author,
                      Title = article.Title,
                      Description = article.Description,
                      ContentSnippet = ArticleSummary.TrimContent(article.Content, 500),
                      PublishedDate = article.PublishedDate.Value,
                      Url = new Url {
                          Id = article.UrlId,
                          Path = article.UrlPath,
                          EntityType = article.UrlEntityType,
                          EntityId = article.UrlEntityId
                      }
                  }).ToList();

                context.Cache["LatestArticles"] = this.data;
          
            } else {
                this.data = latestArticles;
            }
        }
    }
}
