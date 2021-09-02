using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using _928.Core.Wrappers;

namespace _928.Core
{
    public static class IocBootstrapper
    {
        public static void Run()
        {
            CommandFactory.Initialize(ObjectFactory.Container);
        }
        public static void Run(IContainer container)
        {
            CommandFactory.Initialize(container);

           // ObjectFactory.Configure(x =>
           //x.For<IMapper>()
           //.Use<MyMapper>());

        }
    }
}
