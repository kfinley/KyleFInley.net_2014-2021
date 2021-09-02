using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using _928.Commands;
using _928.Data.Repository;
using KyleFinley.Models;
using _928.Data.EntityFramework;
using _928.UrlShortener;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Entities.Models;

namespace KyleFinley.Commands {
    public class CommandsRegistry : _928.Commands.CommandsRegistry {

        public CommandsRegistry()
            : base() {
            
            For<IRepository<Image>>().Use<Repository<Image>>();
            //For<IRepository<SiteUrl>>().Use<Repository<SiteUrl>>();
            For<IRepository<Article>>().Use<Repository<Article>>();
            For<IRepository<Category>>().Use<Repository<Category>>();
            For<IRepository<Home>>().Use<Repository<Home>>();
            For<IRepository<Page>>().Use<Repository<Page>>();

            //For<GetHeaderImage>().Use<GetHeaderImage>();
           
        }
    }
}
