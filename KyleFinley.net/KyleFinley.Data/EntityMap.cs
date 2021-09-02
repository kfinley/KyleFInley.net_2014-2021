using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Data {
    public class EntityMap : EntityTypeConfiguration<Entity> {
         
        public EntityMap() {

            ToTable("Entities");
            HasKey(p => p.Id).Property(p => p.Id);

            Property(p => p.CreatedDate).HasColumnName("DateCreated");
            Property(p => p.Name);
            //Property(p => p.Enabled);
            Property(p => p.EntityType);



            //Map(x => {
            //    x.ToTable("Entities");
            //});

            //Ignore(p => p.EntityType);  // Ignored because it's mapped in child mappings above and will throw an error if not.
            //Property(p => p.CreatedDate).IsRequired();
            
            //Ignore(p => p.Name); 
        }
    }
}
