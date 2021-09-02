using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Interfaces {
    public interface IApplicationContext {
        object this[string name] {
            get;
            set;
        }
    }
}
