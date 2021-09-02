using _928.Core;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class SaveEntity : BaseDataSourcedCommand<Entity>, ICommand {

        public SaveEntity(IRepository<Entity> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            if (this.Data.Id != Guid.Empty) {
                this.data = repository.Update(this.data);
            } else {
                this.Data.DateCreated = DateTime.Now;

                this.data = repository.Insert(this.data);
            }
        }
    }
}
