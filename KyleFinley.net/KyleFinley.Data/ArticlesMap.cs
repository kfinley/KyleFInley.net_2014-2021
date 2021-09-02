using KyleFinley.Models;
using System.Data.Entity.ModelConfiguration;

namespace KyleFinley.Data.Mappings {
    public class ArticlesMap : EntityTypeConfiguration<Article> {

        public ArticlesMap() {

            ToTable("Articles");
            HasKey(p => p.Id).Property(p => p.Id);
            Property(p => p.Author).IsRequired();
            Property(p => p.Headline).IsRequired().HasMaxLength(100);
            Property(p => p.AlternativeHeadline).IsRequired().HasMaxLength(200);
            
        }
    }
}
