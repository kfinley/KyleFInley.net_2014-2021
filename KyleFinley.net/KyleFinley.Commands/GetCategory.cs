using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using KyleFinley.Models;
using _928.Data.Repository;
using _928.Core.Interfaces;
using _928.Entities.Models;

namespace KyleFinley.Commands {

    public class GetCategory : BaseDataSourcedCommand<Category>, IDataCommand<Category> {

        public GetCategory(IRepository<Category> repository, IHttpContext context)
            : base(repository, context) {

        }

        public bool RetrieveShareUrlStats { get; set; }

        public void Execute() {
            try {

                this.data = this.Query().FirstOrDefault();

            } catch (Exception ex) {
                throw new Exception("Error retrieving Catigory ID: {0}. Message: {1}".FormatWith(this.Id, ex.Message), ex);
            }
        }


        public IQueryable<Category> Query() {
            return (from p in base.repository.All()
                    where p.Id == this.Id
                    select p);
        }
    }
}
