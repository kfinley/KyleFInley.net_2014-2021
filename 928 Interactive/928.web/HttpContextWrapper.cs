using _928.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Web {
    public class HttpContextWrapper : IHttpContext {

        private IApplicationContext appContext = new ApplicationContextWrapper();
        private IRequestContext requestContext = new RequestContextWrapper();
        private ICache cache = new CachWrapper();

        public IApplicationContext AppContext {
            get { return this.appContext; }
        }

        public IRequestContext RequestContext {
            get { return this.requestContext; }
        }

        public ICache Cache {
            get { return this.cache;  }
        }
    }
}
