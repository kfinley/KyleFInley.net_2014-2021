using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Data.EntityFramework.EntityMappings {
    public class CoreExceptionMap : EntityTypeConfiguration<CoreException> {
        public CoreExceptionMap() {
            ToTable("Exceptions");
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.DateCreated);
            Property(p => p.ExceptionType);
            Property(p => p.ExceptionMessage);
            Property(p => p.AdditionalData);

        }
    }
}
