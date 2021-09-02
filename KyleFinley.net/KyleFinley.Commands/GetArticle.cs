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
    public class GetArticle : BaseDataSourcedCommand<Article>, IDataCommand<Article> {

        public GetArticle(IRepository<Article> repository, IHttpContext context)
            : base(repository, context) {
        }

        public bool RetrieveShareUrlStats { get; set; }

        public void Execute() {

            try {
                this.data = this.Query().FirstOrDefault();
                //this.data = article as Article;

            } catch (Exception ex) {
                throw new Exception("Error retrieving Article ID: {0}, url: {1}. Message: {2}".FormatWith(this.Id, this.Url.HasValue() ? this.Url : "Not Provided", ex.Message), ex);
            }
        }

        public string Url { get; set; }


        public IQueryable<Article> Query() {
            return (from u in base.repository.All()
                    where u.Id == this.Id
                    select u);
        }
    }
}
