using _928.Commands;
using _928.Core;
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
    public class GetUrls : BaseDataSourcedCommand<IList<Url>, Url>, ICommand {

        public GetUrls(IRepository<Url> repository, IHttpContext context)
            : base(repository, context) {
        }

        public IQueryable<Url> Query() {
            return repository.All();
        }

        public void Execute() {

            this.Data = this.Query().ToList();

        }
    }
}
