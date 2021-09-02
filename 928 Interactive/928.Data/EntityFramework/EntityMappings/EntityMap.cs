using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Entities;

namespace _928.Data.EntityFramework.EntityMappings {
    public class EntityMap : EntityTypeConfiguration<Entity> {
        public EntityMap() {

            ToTable("Entities");
            HasKey(p => p.Id).Property(p => p.Id);

            Property(p => p.CreatedDate).HasColumnName("DateCreated");
            Property(p => p.LastModified);
            Property(p => p.Name);
            //Property(p => p.Enabled);
            Property(p => p.EntityType);

        }
    }
}
