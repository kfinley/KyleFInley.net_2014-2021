using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using _928.Data.Repository;
using _928.Entities;
using _928.Data.EntityFramework;
using StructureMap.Configuration.DSL;
using _928.Core.Wrappers;
using _928.Models;

namespace _928.Commands {
    public class CommandsRegistry : Registry {
        public CommandsRegistry() {

            For<ICommandDispatcher>().Use<CommandDispatcher>();

            For<IRepository<EntityAssociation>>().Use<Repository<EntityAssociation>>();
            //For<IRepository<Entity>>().Use<Repository<Entity>>();
            For<IRepository<Url>>().Use<Repository<Url>>();
            For<IRepository<CoreException>>().Use<Repository<CoreException>>();
            For<IRepository<Redirect>>().Use<Repository<Redirect>>();

           // For<IMapper>().Use<MyMapper>();
        }
    }
}
