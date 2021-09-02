using _928.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Data.EntityFramework.EntityMappings {
    public class UrlsMap : EntityTypeConfiguration<Url> {

        public UrlsMap() {
            ToTable("SiteUrls");
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Path).HasColumnName("Url").IsRequired().HasMaxLength(50);
            Property(p => p.EntityId);
            Property(p => p.EntityType);

            //Ignore(p => p.Description);
            //Ignore(p => p.LastModified);

        }
    }
}
