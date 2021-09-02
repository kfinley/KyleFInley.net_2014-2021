using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using StructureMap;
using StructureMap.Graph;
using KyleFinley.Models;
using KyleFinley.Data;
using StructureMap.Pipeline;
using StructureMap.Web;
using _928.Core.UrlShortener;
using _928.Data.Repository;
using _928.Data.EntityFramework;
using _928.Core.Interfaces.Data;

namespace KyleFinley.Commands {


    public static class IocBootstrapper {
        public static void Run() {

            ObjectFactory.Configure(x => {
                x.Scan(a => {
                    a.AssembliesFromApplicationBaseDirectory();
                    a.WithDefaultConventions();
                });
            });

            ObjectFactory.Configure(x =>
              x.For<IRepository<Image>>()
              .Use<Repository<Image>>());

            ObjectFactory.Configure(x =>
              x.For<IRepository<Redirect>>()
              .Use<Repository<Redirect>>());

            ObjectFactory.Configure(x =>
              x.For<IRepository<VanityUrl>>()
              .Use<Repository<VanityUrl>>());

            ObjectFactory.Configure(x =>
                x.For<IRepository<Article>>()
                .Use<Repository<Article>>());

            ObjectFactory.Configure(x =>
                x.For<IUnitOfWork>()
                .Use<EFUnitOfWork>()
                );

            ObjectFactory.Configure(x =>
                x.For<IDbContext>()
                .Use<DataContext>()
                .Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["KyleFinley"].ConnectionString));

            ObjectFactory.Configure(x =>
                x.For<IUrlShortenerService>()
                .Use<GoogleUrlShortenerService>()
                .Ctor<string>("apiKey")
                .Is("AIzaSyDZhm5LlPLpK7GkHT9iob1Xp8I0tDTmE9k")
                .Ctor<string>("appName")
                .Is("KyleFinley.net"));
        }
    }
}
