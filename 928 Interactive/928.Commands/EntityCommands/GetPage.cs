using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Commands;
using _928.Core.Interfaces;
using _928.Data.Repository;

using _928.Entities.Models;
using _928.Entities;


namespace _928.Commands {

    public class GetPage<T> : BaseDataSourcedCommand<Page<T>, Page>, ICommand
        where T : IEntity {


        public GetPage(IRepository<Page> repository, IHttpContext context)
            : base(repository, context) {

                this.CacheKey = "Pages_{0}".FormatWith(this.GetType().GenericTypeArguments[0].Name);
        }

        public IDataCommand<T> EntityCommand { get; set; }

        public bool RetrieveShareUrlStats { get; set; } = false;

        public void Execute() {
            try {

                this.data = base.RetrieveFromCache(Id.ToString(), GetPageAndEntity);

                if (this.data.IsNotNull() && this.RetrieveShareUrlStats) {

                    var getSocialSharesUrlStats = CommandFactory.Create<GetSocialSharesUrlStats>();
                    getSocialSharesUrlStats.Data = this.data;

                    dispatcher.Run(getSocialSharesUrlStats, false);
                }

            } catch (Exception ex) {
                throw new Exception("Error retrieving Page ID: {0}. Message: {1}".FormatWith(this.Id, ex.Message), ex);
            }
        }

        private Page<T> GetPageAndEntity() {

            var page = (from p in base.repository.All()
                        where p.Id == this.Id
                        select p).FirstOrDefault();

            this.data = page.MapTo(new Page<T>());

            if (this.EntityCommand != null) {
                EntityCommand.Id = this.data.EntityId;
                dispatcher.Run(EntityCommand);
                this.data.Entity = EntityCommand.Data;
            }

            return this.data;
        }
    }
}
