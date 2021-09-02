using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Data.EntityFramework.EntityMappings {
    public class EntityMap : EntityTypeConfiguration<Entity> {
        public EntityMap() {
            ToTable("Entity");
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.DateCreated);
            Property(p => p.Name);
            Property(p => p.Enabled);
        }
    }
}
