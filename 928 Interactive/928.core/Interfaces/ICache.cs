using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Interfaces {
    public interface ICache {

        object this[string name] {
            get;
            set;
        }

        int Count { get; }
    }
}
