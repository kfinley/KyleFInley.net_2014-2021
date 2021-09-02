
using _928.Entities;
using _928.Core.Interfaces;
using System.Collections.Generic;

namespace KyleFinley.Models {
    public class Article : Entity, IEntity {
        
        public string Author { get; set; }
        public string Headline { get; set; }
        public string AlternativeHeadline { get; set; }
        public bool ContainsCode { get; set; }

        public override int EntityType {
            get {
                return (int)Models.EntityType.Article;
            }
        }

       // public IList<Category> Categories { get; set; }
    }
}