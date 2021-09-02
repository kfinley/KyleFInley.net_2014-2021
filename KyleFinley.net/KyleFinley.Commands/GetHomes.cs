
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using _928.Entities.Models;
using _928.Core.Linq;

namespace KyleFinley.Commands {
    public class GetHomes :  BaseDataSourcedCommand<IList<IEntity>, Home>, IQueryableCommand {

        private IOrderByExpression<Home> orderBy = new OrderByExpression<Home, Guid>(a => a.Id);

        public GetHomes(IRepository<Home> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void OrderBy<TKeyType>(OrderByExpression<Home, TKeyType> expression) {
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
