using _928.Core.Interfaces;
using _928.Core.RestClient;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands
{
    public abstract class BaseServiceSourcedCommand<T> : BaseCommand<T> 
       // where T : ModelBase
    {
        public BaseServiceSourcedCommand(IRestClient client, IHttpContext context)
            : base(context) {
            this.restClient = client;
        }

        protected IRestClient restClient;

    }
}
