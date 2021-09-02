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

    public class GetPage<T> : BaseDataSourcedCommand<T>, ICommand
        where T : Page, new() {

        public GetPage(IRepository<T> repository, IHttpContext context)
            : base(repository, context) {

        }

        public bool RetrieveShareUrlStats { get; set; }

        public void Execute() {
            try {

                var cachedPage = (T)context.Cache[Id.ToString()];

                if (cachedPage == null) {
                    this.data = (from p in base.repository.All()
                                 where p.Id == this.Id
                                 select p).FirstOrDefault();

                    context.Cache[Id.ToString()] = this.data;
                } else {
                    this.data = cachedPage;
                }

                if (this.data.IsNotNull() && this.RetrieveShareUrlStats) {

                    var getSocialSharesUrlStats = CommandFactory.Create<GetSocialSharesUrlStats>();
                    getSocialSharesUrlStats.Data = this.data;

                    dispatcher.Run(getSocialSharesUrlStats, false);
                }

            } catch (Exception ex) {
                throw new Exception("Error retrieving Page ID: {0}. Message: {1}".FormatWith(this.Id, ex.Message), ex);
            }
        }
    }
}
