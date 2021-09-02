using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Interfaces {
    public interface IHttpContext {
        IApplicationContext AppContext { get; }
        IRequestContext RequestContext { get;  }
        ICache Cache { get; }
    }
}
