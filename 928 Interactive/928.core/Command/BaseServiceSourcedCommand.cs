using _928.Core.Data;
using _928.Core.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Command
{
    public abstract class BaseServiceSourcedCommand<T> : BaseCommand<T>
    {
        public BaseServiceSourcedCommand(IRestClient client)
        {
            this.restClient = client;
        }

        protected IRestClient restClient;

    }
}
