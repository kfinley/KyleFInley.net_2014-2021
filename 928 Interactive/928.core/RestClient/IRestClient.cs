using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.RestClient
{
    public interface IRestClient
    {
        string BaseUrl { set; }
        string UserAgent { set; }

        void AddParamater(string name, object value);
        void AddParamater(string name, object value, RequestParameterType paramType);

        void AddHeader(string name, string value);

        void SetRootElement(string name);

        void SetAcceptType(RestClient.AcceptTypes acceptType);

        string Execute();
        T Execute<T>() where T : new();
    }
}
    