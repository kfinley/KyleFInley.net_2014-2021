using System;
using System.Data.Entity.ModelConfiguration;

using _928.Entities;
using _928.Models;


namespace _928.Data.EntityFramework.EntityMappings {
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
