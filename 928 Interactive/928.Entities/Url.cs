using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Entities {
    public class Url : EntityBase {
        
        public string Path { get; set; }
        public Guid EntityId { get; set; }
        public virtual int EntityType { get; set; }
      //  public DateTime? LastModified { get; set; }

    }
}
