using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _928.Entities;
using _928.Models;


namespace KyleFinley.Data.Mappings {
    public class RedirectsMap : EntityTypeConfiguration<Redirect> {

        public RedirectsMap() {
            ToTable("Redirects");
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.OldPath).IsRequired().HasMaxLength(500);
            Property(p => p.NewPath).IsRequired().HasMaxLength(500);
            //Property(p => p.EntityType).IsRequired();
            
            Ignore(p => p.Do);
        }
    }
}
