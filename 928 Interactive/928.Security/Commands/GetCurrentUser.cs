using _928.Commands;
using _928.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace _928.Security.Commands {
    public class GetCurrentUser: BaseCommand<IUser>, ICommand {

        public GetCurrentUser(IHttpContext context)
            : base(context) {
        }

        public void Execute() {

            this.data = new AppUser() {

                Id = Guid.Parse(context.RequestContext.User.Identity.GetUserId()),
                UserName = context.RequestContext.User.Identity.Name,
                Email = context.RequestContext.User.Identity.Name
            };
        }
    }
}
