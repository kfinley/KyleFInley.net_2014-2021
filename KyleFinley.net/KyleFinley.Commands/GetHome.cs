
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

namespace KyleFinley.Commands {
    public class GetHome : BaseDataSourcedCommand<Home>, IDataCommand<Home> {

        public GetHome(IRepository<Home> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            try {
                this.data = this.Query().FirstOrDefault();

                //this.data = homee as Home;

            } catch (Exception ex) {
                throw new Exception("Error retrieving Article ID: {0}, url: {1}. Message: {2}".FormatWith(this.Id, this.Url.HasValue() ? this.Url : "Not Provided", ex.Message), ex);
            }
        }

        public string Url { get; set; }


        public IQueryable<Home> Query() {
            return (from u in base.repository.All()
                    where u.Id == this.Id
                    select u);
        }
    }
}
