using _928.Core.Interfaces.Data;
using _928.Core;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;


namespace _928.Data.EntityFramework {

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
            try {
                return base.SaveChanges();
            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
#if DEBUG
                foreach (var error in ex.EntityValidationErrors) {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ", error.Entry.Entity.GetType().Name, error.Entry.State);
                    foreach (var ve in error.ValidationErrors) {
                        System.Diagnostics.Debug.WriteLine("-Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        System.Diagnostics.Debug.WriteLine("-Value: \"{0}\"", error.Entry.Entity.GetType().GetProperty(ve.PropertyName).GetValue(error.Entry.Entity, null));
                        System.Diagnostics.Debug.WriteLine("Object Properties:");
                        foreach (var propInfo in error.Entry.Entity.GetType().GetProperties()) {
                            string name = propInfo.Name;
                            object value = propInfo.GetValue(error.Entry.Entity);
                            Console.WriteLine("{0}={1}", name, value);
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine(string.Empty);

#endif
                throw;
            }
        }

        public ITransaction BeginTransaction() {
            return new DbContextTransactionWrapper(base.Database.BeginTransaction());
        }

        public new void Dispose() {
            base.Dispose();
        }
    }

    public class DataContext : DbContextBase {

        List<Type> typesToRegister;
        
        static DataContext() {
            Database.SetInitializer<DataContext>(null);
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {

        }

        public DataContext(string nameOrConnectionString, Assembly mappingAssembly)
            : base(nameOrConnectionString) {

            this.typesToRegister = mappingAssembly.GetTypes()
             .Where(type => !String.IsNullOrEmpty(type.Namespace))
             .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();

            var buildInEntityConfigurations = Assembly.GetExecutingAssembly().GetTypes()
                  .Where(type => !String.IsNullOrEmpty(type.Namespace))
                   .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();

            this.typesToRegister.AddRange(buildInEntityConfigurations);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            
            foreach (var type in typesToRegister.GroupBy(t => t.Name).Select(grp => grp.First())) {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

           // modelBuilder.Conventions.Add<UnmappedTypesConvention>();

        }
    }

    internal class DbContextTransactionWrapper : ITransaction {

        private DbContextTransaction transaction;

        public DbContextTransactionWrapper(DbContextTransaction transaction) {
            this.transaction = transaction;
        }
        public void Commit() {
            transaction.Commit();
        }

        public void Rollback() {
            transaction.Rollback();
        }

        public void Dispose() {
            transaction.Dispose();
        }
    }
}
