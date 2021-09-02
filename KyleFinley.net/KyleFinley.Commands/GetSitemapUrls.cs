using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;

using KyleFinley.Models;
using _928.Entities;
using _928.Entities.Models;

namespace KyleFinley.Commands {

    public class GetSitemapUrls : BaseDataSourcedCommand<IList<Page>, Page>, ICommand {

        public GetSitemapUrls(IRepository<Page> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            this.data = repository.All()
                        .Where(p => p.Enabled)
                        .OrderByDescending(p => p.PublishedDate)
                        .Select(p => p).ToList();

            // Add a URL for the root
            this.data.Insert(0, new Page {
                Path = ""
            });
        }
    }
}
