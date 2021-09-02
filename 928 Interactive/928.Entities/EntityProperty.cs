using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Entities {
    public class EntityProperty : EntityBase {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public Guid EntityId { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }
    }
}
