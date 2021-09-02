
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
using KyleFinley.Models;
using _928.Entities.Models;

namespace KyleFinley.Commands {
    public class GetPages<T> : BaseDataSourcedCommand<IList<T>, T>, ICommand
        where T : Page {

        private IOrderByExpression<T> orderBy = new OrderByExpression<T, Guid>(a => a.Id);

        public GetPages(IRepository<T> repository, IHttpContext context)
            : base(repository, context) {
        }

        public bool RetrieveEnabledOnly { get; set; }

        public EntityType EntityType { get; set; }

        public void OrderBy<TKeyType>(OrderByExpression<T, TKeyType> expression) {
            this.orderBy = expression;
        }

        internal IQueryable<T> Query() {
            if (this.RetrieveEnabledOnly) {
                return orderBy.ApplyOrdering(from p in repository.All()
                                             where p.Enabled == true
                                             select p);
            } else {
                return orderBy.ApplyOrdering(from p in repository.All()
                                             select p);
            }
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
