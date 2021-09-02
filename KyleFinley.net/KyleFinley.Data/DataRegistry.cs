using _928.Core.Interfaces.Data;
using _928.Data.EntityFramework;
using System.Configuration;
using System.Reflection;
using StructureMap;

namespace KyleFinley.Data {
    public class DataRegistry : Registry {

        public DataRegistry() {
            
            For<IUnitOfWork>().Use<EFUnitOfWork>();
            For<IDbContext>().Use<DataContext>()
                .Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["KyleFinley"].ConnectionString)
                .Ctor<Assembly>("mappingAssembly").Is(Assembly.GetExecutingAssembly());
            
        }
    }
}
