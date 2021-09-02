using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Core.Linq;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class GetArticles : BaseDataSourcedCommand<IList<IEntity>, Article>, IQueryableCommand {

        private IOrderByExpression<Article> orderBy = new OrderByExpression<Article, Guid>(a => a.Id);

        public GetArticles(IRepository<Article> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void OrderBy<TKeyType>(OrderByExpression<Article, TKeyType> expression) {
            this.orderBy = expression;
        }

        public IQueryable<IEntity> Query() {
            return orderBy.ApplyOrdering(repository.All());
        }

        public void Execute() {

            try {
                this.data = this.Query().ToList();
            } catch (Exception ex) {
                throw new Exception("Error retrieving list. Message: {0}".FormatWith(ex.Message), ex);
            }
        }
    }
}
