using _928.Core.Interfaces;
using _928.Core.Linq;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class GetExceptions: BaseDataSourcedCommand<IList<CoreException>, CoreException>, ICommand {


        private IOrderByExpression<CoreException> orderBy = new OrderByExpression<CoreException, DateTime>(a => a.DateCreated, SortOrder.Descending);

        public GetExceptions(IRepository<CoreException> repository, IHttpContext context)
            : base(repository, context) {
        }

        internal IQueryable<CoreException> Query() {
            return orderBy.ApplyOrdering(repository.All());
        }

        public void Execute() {

            this.Data = this.Query().Take(25).ToList();

        }
    }
}
