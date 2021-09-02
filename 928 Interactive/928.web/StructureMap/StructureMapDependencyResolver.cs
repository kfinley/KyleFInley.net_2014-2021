using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using System.Web.Http.Dependencies;

namespace _928.Web.StructureMap
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        private IContainer container;

        public object GetService(Type serviceType)
        {
            object instance = container.TryGetInstance(serviceType);

            if (instance == null && !serviceType.IsAbstract)
            {
                container.Configure(c => c.AddType(serviceType, serviceType));
                instance = container.TryGetInstance(serviceType);
            }

            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope() {
            var childContainer = this.container.GetNestedContainer();
            return new StructureMapScope(childContainer);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }

    public class StructureMapScope : IDependencyScope {
        private readonly IContainer container;

        public StructureMapScope(IContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType == null) {
                return null;
            }

            if (serviceType.IsAbstract || serviceType.IsInterface) {
                return this.container.TryGetInstance(serviceType);
            }

            return this.container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return this.container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose() {
            this.container.Dispose();
        }
    }
}
