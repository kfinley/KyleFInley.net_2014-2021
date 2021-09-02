using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;

namespace _928.Commands {
    public class GetUrl : BaseDataSourcedCommand<Url>, ICommand {

        public GetUrl(IRepository<Url> repository, IHttpContext context)
            : base(repository, context) {
            base.CacheKey = "SiteUrls";
        }

        public string Url { get; set; }
        public Guid EntityId { get; set; }

        public void Execute() {

            Url siteUrl;

            if (Url.IsNull() == false) {

                siteUrl = base.RetrieveFromCache(Url.ToLower(),
                                                 () => (from u in base.repository.All()
                                                        where u.Path.ToLower() == this.Url.ToLower()
                                                        select u).FirstOrDefault(),
                                                 s => (new string[] { s.Id.ToString(), s.EntityId.ToString(), s.Path.ToLower() }));

            } else {

                if (this.Id != Guid.Empty) {

                    siteUrl = base.RetrieveFromCache(this.Id.ToString(),
                                                        () => (from u in base.repository.All()
                                                               where u.Id == this.Id
                                                               select u).FirstOrDefault(),
                                                        s => (new string[] { s.Id.ToString(), s.EntityId.ToString(), s.Path.ToLower() }));
                } else {

                    siteUrl = base.RetrieveFromCache(this.EntityId.ToString(),
                                                        () => (from u in base.repository.All()
                                                               where u.EntityId == this.EntityId
                                                               select u).FirstOrDefault(),
                                                        s => (new string[] { s.Id.ToString(), s.EntityId.ToString(), s.Path.ToLower() }));
                }
            }

            if (siteUrl != null)
                this.Data = siteUrl;
        }
    }
}
