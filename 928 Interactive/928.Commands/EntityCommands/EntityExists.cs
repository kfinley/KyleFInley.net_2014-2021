using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands.EntityCommands {
    public class EntityExists : BaseDataSourcedCommand<bool, Entity>, ICommand {

        public EntityExists(IRepository<Entity> repository, IHttpContext context)
            : base(repository, context) {
        }

        public string Name { get; set; }
        public int EntityType { get; set; }

        public void Execute() {

            var entity = (from e in repository.All()
                          where e.Name == this.Name &&
                          EntityType > 0 ? e.EntityType == EntityType : e.EntityType > 0
                          select e).FirstOrDefault();

            if (entity == null) {
                entity = (from e in repository.Local(e => e.Name == this.Name && EntityType > 0 ? e.EntityType == EntityType : e.EntityType > 0) select e).FirstOrDefault();
            }

            if (entity == null)
                this.data = false;
            else
                this.data = true;
        }
    }
}
