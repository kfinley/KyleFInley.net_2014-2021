using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

using _928.Core.Interfaces.Data;

namespace KyleFinley.Data {

    public abstract class DbContextBase : DbContext, IDbContext, IDataContext, IDisposable {

        public DbContextBase(string nameOrConnectionString) :
            base(nameOrConnectionString) {
            Configuration.LazyLoadingEnabled = false;
        }

        public new IDbSet<T> Set<T>() where T : class {
            return base.Set<T>();
        }

        public new DbEntityEntry Entry(object entity) {
            return base.Entry(entity);
        }

        public override int SaveChanges() {
            return base.SaveChanges();
        }

        public new void Dispose() {
            base.Dispose();
        }
    }

    public class DataContext : DbContextBase {

        static DataContext() {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
               .Where(type => !String.IsNullOrEmpty(type.Namespace))
               .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister) {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
