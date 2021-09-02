using KyleFinley.Models;
using System.Data.Entity.ModelConfiguration;

namespace KyleFinley.Data {
    class HomeMap  : EntityTypeConfiguration<Home> {

        public HomeMap() {

            ToTable("Home");
            HasKey(p => p.Id).Property(p => p.Id);
        }
    }
}