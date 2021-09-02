using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class SaveException: BaseDataSourcedCommand<CoreException>, ICommand {

        public SaveException(IRepository<CoreException> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            try {
                this.data.DateCreated = DateTime.Now;

                this.data = repository.Insert(this.data);
            } catch (Exception ex)
            {
                //TODO: Send email that exception has occurred.
            }
        }
    }
}
