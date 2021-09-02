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
using _928.Entities;

namespace KyleFinley.Commands {
    public class HandleRouting : BaseDataSourcedCommand<Url>, ICommand {

        public HandleRouting(IRepository<Url> repository, IHttpContext context)
            : base(repository, context) {
        }

        public HandleRouting(IRepository<Url> repository, IHttpContext context, string url)
            : base(repository, context) {
            this.UrlString = url;
        }

        public string UrlString { get; set; }

        public void Execute() {

            var getSiteUrl = CommandFactory.Create<GetUrl>();
            getSiteUrl.Url = this.UrlString;

            dispatcher.Run(getSiteUrl);

            var url = getSiteUrl.Data;

            if (url != null) {

                context.RequestContext.SetRouteData("controller", "Content");
                context.RequestContext.SetRouteData("action", ((EntityType)url.EntityType).ToString());
                context.RequestContext.SetRouteData("id", url.Id);
                context.RequestContext.SetRouteData("entityId", url.EntityId);
                
            } else {
                context.RequestContext.SetRouteData("controller", this.UrlString);

            }
        }
    }


    //TODO: Look back at this and see if it's possible to get the CommandFactory to new this kind of thing up with only knowing a Type
    // That way we can handle the routing bits back in the HttpHandler instead of here.
    //public class HandleTopLevelRouting : BaseCommand<dynamic>, ICommand {

    //    public string UrlString { get; set; }

    //    public void Execute() {

    //        var getVanityUrl = CommandFactory.Create<GetVanityUrl>();
    //        getVanityUrl.Url = this.UrlString;

    //        dispatcher.Run(getVanityUrl);

    //        var url = getVanityUrl.Data;

    //        if (url != null) {
    //            switch (url.EntityType) {
    //                case Models.EntityType.Article:
    //                    this.Data = new {
    //                        controller = "Article",
    //                        action = "Index",
    //                        id = url.Id
    //                    };
    //                    break;
    //                case Models.EntityType.Album:
    //                    this.Data = new {
    //                        controller = "Album",
    //                        action = "Index",
    //                        id = url.Id
    //                    };
    //                    break;
    //                case Models.EntityType.Photo:
    //                    this.Data = new {
    //                        controller = "Photo",
    //                        action = "Index",
    //                        id = url.Id
    //                    };
    //                    break;
    //                /*
    //                 * Finish out the entity type conversions
    //                 * 
    //                 */
    //                default:
    //                    break;
    //            }
    //        } else {
    //            this.Data = new {
    //                controller = this.UrlString,
    //            };
    //        }
    //    }
    //}
}
