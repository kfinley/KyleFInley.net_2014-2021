using _928.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Data.EntityFramework.EntityMappings {
    public class PagesMap : EntityTypeConfiguration<Page> {

        public PagesMap() {
            ToTable("Pages");
            HasKey(p => p.Id).Property(p => p.Id);

            Ignore(p => p.CreatedBy);
          
        }
    }
}
