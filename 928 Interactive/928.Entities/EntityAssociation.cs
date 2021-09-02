using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Entities {
    public class EntityAssociation : EntityBase {

        public Guid EntityId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
    }
}
