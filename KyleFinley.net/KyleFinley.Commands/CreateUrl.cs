using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class CreateUrl : BaseDataSourcedCommand<SiteUrl>, ICommand {

        public CreateUrl(IRepository<SiteUrl> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            try { 
            this.data.Path = this.data.Path.ToLower();

            repository.Insert(this.data);

            } catch (Exception ex) {
                throw new Exception("Error creating site Url for {0}. Message: {1}".FormatWith(this.data.Path, ex.Message), ex);
            }
        }
    }
}
