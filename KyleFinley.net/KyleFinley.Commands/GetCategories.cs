using _928.Commands;
using _928.Core;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class GetCategories : BaseDataSourcedCommand<IList<IEntity>, Category>, IQueryableCommand {

        public GetCategories(IRepository<Category> repository, IHttpContext context)
            : base(repository, context) {

        }

        public IQueryable<IEntity> Query() {
            return from c in repository.All()
                   select c;
           
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
