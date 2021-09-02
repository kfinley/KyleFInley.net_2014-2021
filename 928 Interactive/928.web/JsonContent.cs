using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _928.Web {
    public class JsonContent : StringContent {

        public JsonContent(string content)
            : base(content, Encoding.UTF8, "application/json") {
        }

        public JsonContent(object content)
            : base(JObject.FromObject(content).ToString(), Encoding.UTF8, "application/json") {

        }
    }
}
