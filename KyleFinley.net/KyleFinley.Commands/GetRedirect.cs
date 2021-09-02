using _928.Commands;
using _928.Core;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class GetRedirect : BaseDataSourcedCommand<Redirect>, ICommand {

        public GetRedirect(IRepository<Redirect> repository, IHttpContext context)
            : base(repository, context) {
        }

        public string OldPath { get; set; }

        public void Execute() {

            var redirect = (from u in base.repository.All()
                           where u.OldPath == this.OldPath
                           select u).FirstOrDefault();

            if (redirect != null) {
                this.Data = new Redirect {
                    OldPath = redirect.OldPath,
                    NewPath = redirect.NewPath,
                    Do = true
                };
            } else {
                this.Data = new Redirect {
                    OldPath = OldPath,
                    Do = false
                };
            }
        }
    }
}
