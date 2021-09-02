using KyleFinley.Models;
using System.Data.Entity.ModelConfiguration;

namespace KyleFinley.Data {
    class CategoryMap : EntityTypeConfiguration<Category> {

        public CategoryMap() {

            ToTable("Categories");
            HasKey(p => p.Id).Property(p => p.Id);
            
        }

    }
}
