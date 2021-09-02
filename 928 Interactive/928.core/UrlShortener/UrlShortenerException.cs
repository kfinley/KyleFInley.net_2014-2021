using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.UrlShortener {
    [Serializable]
    public class UrlShortnerException : Exception {
        public UrlShortnerException() { }
        public UrlShortnerException(string message) : base(message) { }
        public UrlShortnerException(string message, Exception inner) : base(message, inner) { }
        protected UrlShortnerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
