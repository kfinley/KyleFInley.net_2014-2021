using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _928.Entities;
using System.Data.Entity.ModelConfiguration;

namespace _928.Data.EntityFramework.EntityMappings {
    class EntityAssociationMap : EntityTypeConfiguration<EntityAssociation> {
        
        public EntityAssociationMap() {
            
            ToTable("EntityAssociations");
            HasKey(p => new { p.Id, EntityId = p.EntityId });
            Property(p => p.Id);
            Property(p => p.EntityId);
            Property(p => p.DateCreated);
            Property(p => p.Active);
        }
    }
}