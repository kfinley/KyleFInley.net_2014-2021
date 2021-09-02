using _928.Core.Interfaces;
using _928.Entities;
using System.Collections.Generic;

namespace KyleFinley.Models {
    public class Category : Entity, IEntity {

        public override int EntityType {
            get {
                return (int)KyleFinley.Models.EntityType.Category;
            }
        }
        
        public IList<ArticleSummary> ArticleSummaries { get; set; }
    }
}
