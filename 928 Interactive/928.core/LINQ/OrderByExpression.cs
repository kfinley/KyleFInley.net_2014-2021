using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Linq {
    public interface IOrderByExpression<T> {
        IQueryable<T> ApplyOrdering(IQueryable<T> query);
    }

    public class OrderByExpression<T, U> : IOrderByExpression<T> {

        private Expression<Func<T, U>> exp = null;
        private SortOrder sortOrder;


        public IQueryable<T> ApplyOrdering(IQueryable<T> query) {

            if (this.sortOrder == SortOrder.Ascending) {
                return query.OrderBy(exp);
            } else {
                return query.OrderByDescending(exp);
            }
        }

        public OrderByExpression(Expression<Func<T, U>> myExpression) {
            sortOrder = SortOrder.Ascending;
            exp = myExpression;
        }

        public OrderByExpression(Expression<Func<T, U>> myExpression, SortOrder sortOrder) {

            exp = myExpression;
            this.sortOrder = sortOrder;

        }
    }
}
