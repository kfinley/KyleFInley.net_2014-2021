using _928.Commands;
using _928.Core;
using _928.Core.Interfaces;
using _928.Core.Linq;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class GetSiteUrls : BaseDataSourcedCommand<IList<SiteUrl>, SiteUrl>, ICommand {

        public GetSiteUrls(IRepository<SiteUrl> repository, IHttpContext context)
            : base(repository, context) {
        }

        internal IQueryable<SiteUrl> Query() {
            return repository.All();
        }

        public void Execute() {

            this.Data = this.Query().ToList();

        }
    }
}
