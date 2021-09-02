using _928.Core;
using _928.Core.Interfaces;
using _928.Commands;
using _928.Core.Linq;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _928.Data.Repository;
using _928.Entities;
using _928.Entities.Models;

namespace KyleFinley.Commands
{
    public class GetCategoryArticles : BaseDataSourcedCommand<IList<ArticleSummary>, Article>, ICommand
    {

        private IOrderByExpression<Page> orderBy = new OrderByExpression<Page, DateTime>(a => a.PublishedDate.Value, System.Data.SqlClient.SortOrder.Descending);

        public GetCategoryArticles(IRepository<Article> repository, IHttpContext context)
            : base(repository, context)
        {
        }

        public void Execute()
        {
            var categoryArticles = context.Cache["CategoryArticles"] as IList<ArticleSummary>;

            if (categoryArticles.IsNull())
            {
                var getAssociatedArticles = CommandFactory.Create<GetAssociatedEntities>();
                getAssociatedArticles.EntityId = this.Id;
                getAssociatedArticles.SharedContext(repository);

                var getPages = CommandFactory.Create<GetPages<Article>>();
                getPages.EntityType = (int)EntityType.Article;
                getPages.SharedContext(repository);

                getPages.EntityCommand = CommandFactory.Create<GetArticles>();
                
                this.data = (from ea in getAssociatedArticles.Query()
                             join article in repository.All()
                                on ea.Id equals article.Id
                             join page in getPages.Query()
                                on article.Id equals page.EntityId
                             where ea.Active
                             orderby page.PublishedDate descending
                             select new 
                             {
                                 Id = article.Id,
                                 Author = article.Author,
                                 Title = page.Title,
                                 Description = page.Description,
                                 Content = page.Content,
                                 PublishedDate = page.PublishedDate,
                                 PageId = page.Id,
                                 UrlPath = page.Path,
                                 EntityType = article.EntityType,
                                 EntityId = page.EntityId
                             }).Take(10).AsEnumerable().Select(a => new ArticleSummary
                             {
                                 Id = a.Id,
                                 Author = a.Author,
                                 Title = a.Title,
                                 Description = a.Description,
                                 ContentSnippet = ArticleSummary.TrimContent(a.Content, 1000),
                                 PublishedDate = a.PublishedDate.Value,
                                 Url = new Url
                                 {
                                     Id = a.PageId,
                                     Path = a.UrlPath,
                                     EntityType = a.EntityType,
                                     EntityId = a.EntityId
                                 }
                             }).ToList();
                
                context.Cache["CategoryArticles"] = this.data;

            }
            else {
                this.data = categoryArticles;
            }
        }
    }
}
