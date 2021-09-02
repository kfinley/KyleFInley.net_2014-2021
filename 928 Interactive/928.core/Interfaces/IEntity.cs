using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Interfaces {

    public interface IEntity {
        Guid Id { get; }
        int EntityType { get; set; }
        //bool Enabled { get; set; }
    }
}
