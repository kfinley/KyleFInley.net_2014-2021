using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _928.Core {
    public static class ObjectFactory {
        private static readonly Lazy<Container> containerBuilder =
                new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container {
            get { return containerBuilder.Value; }
        }

        private static Container DefaultContainer() {
            return new Container(x => {
                // default config
            });
        }
    }
}
    