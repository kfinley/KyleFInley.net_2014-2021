using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _928.Entities;

namespace KyleFinley.Data.Mappings {
    public class SiteUrlsMap : EntityTypeConfiguration<SiteUrl> {

        public SiteUrlsMap() {
            ToTable("SiteUrls");
            HasKey(p => p.Id).Property(p => p.Id);
            //Property(p => p.Path).HasColumnName("Url").IsRequired().HasMaxLength(50);
            
            Ignore(p => p.LastModified);
            //Ignore(p => p.Description);

            //Ignore<Entity>(e => e.EntityId);
        }
    }
}
