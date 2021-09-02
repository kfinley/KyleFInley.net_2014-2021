using _928.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using Microsoft.AspNet.Identity;
using _928.Security;
using _928.Security.Commands;

namespace _928.Web.Mvc {
    public abstract class BaseApiController : ApiController {
        //TODO: Fix this to inject the ApplictionContextWrapper
        protected ICommandDispatcher dispatcher = new CommandDispatcher(new HttpContextWrapper());
        private static readonly Type CurrentUserKey = typeof(IUser);

        public BaseApiController(ICommandDispatcher dispatcher) {
            this.dispatcher = dispatcher;
        }

        public string CurrentUserName {
            get {
                return (HttpContext.Current.User == null) ? null : HttpContext.Current.User.Identity.Name;
            }
        }

        public IUser CurrentUser {
            get {

                if (!string.IsNullOrEmpty(CurrentUserName)) {
                    IUser user = HttpContext.Current.Items[CurrentUserKey] as IUser;

                    if (user == null) {

                        var getUser = CommandFactory.Create<GetCurrentUser>();
                        dispatcher.Run(getUser, false);

                        user = getUser.Data;

                        if (user != null) {
                            //try
                            //{
                            //    if (!user.IsLockedOut)
                            //    {
                            //        user.LastActivityAt = SystemTime.Now();

                            //    }
                            //}
                            //catch (Exception e)
                            //{
                            //   // Log.Exception(e);
                            //}

                            HttpContext.Current.Items[CurrentUserKey] = user;
                        }
                    }

                    return user;
                }

                return null;
            }
        }
    }
}
