using _928.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _928.Web {
    public class ApplicationContextWrapper : IApplicationContext {

        public object this[string name] {
            get {
                
                return HttpContext.Current.Application[name];
            }
            set {
                
                HttpContext.Current.Application[name] = value;
            }
        }

    }
}
