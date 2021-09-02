
using _928.Core.Interfaces;
using _928.Entities;

namespace KyleFinley.Models {
    public class Home : Entity, IEntity {
        
        public override int EntityType {
            get {
                return (int)Models.EntityType.Home;
            }
        }
    }
}
