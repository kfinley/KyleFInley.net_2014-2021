using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
using _928.Core;

using KyleFinley.Models;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;

namespace KyleFinley.Commands {
    public class GetSiteUrl : BaseDataSourcedCommand<SiteUrl>, ICommand {

        public GetSiteUrl(IRepository<SiteUrl> repository, IHttpContext context)
            : base(repository, context) {
        }

        public string Url { get; set; }
        public Guid EntityId { get; set; }

        public void Execute() {

            SiteUrl siteUrl;

            var cachedUrls = context.Cache["SiteUrls"] as Dictionary<string, SiteUrl>;

            if (cachedUrls.IsNull()) {
                cachedUrls = new Dictionary<string, SiteUrl>();

                context.Cache["SiteUrls"] = cachedUrls;
            }

            if (Url.IsNull() == false) {

                cachedUrls.TryGetValue(Url.ToLower(), out siteUrl);

                if (siteUrl.IsNull()) {
                    siteUrl = (from u in base.repository.All()
                           where u.Path.ToLower() == this.Url.ToLower()
                           select u).FirstOrDefault();

                    if (siteUrl.IsNotNull()) {
                        cachedUrls[this.Url.ToLower()] = siteUrl;
                        cachedUrls[siteUrl.EntityId.ToString()] = siteUrl;

                        context.Cache["SiteUrls"] = cachedUrls;
                    }
                }
            } else {

                cachedUrls.TryGetValue(this.EntityId.ToString(), out siteUrl);

                if (siteUrl.IsNull()) {
                    siteUrl = (from u in base.repository.All()
                               where u.EntityId == this.EntityId
                               select u).FirstOrDefault();

                    cachedUrls[this.EntityId.ToString()] = siteUrl;
                    cachedUrls[siteUrl.Path] = siteUrl;

                    context.Cache["SiteUrls"] = cachedUrls;

                }
            }

            if (siteUrl != null)
                this.Data = siteUrl;
        }
    }
}
