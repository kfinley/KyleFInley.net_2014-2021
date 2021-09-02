using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Commands {
    public class GetAssociatedEntities: BaseDataSourcedCommand<IList<EntityAssociation>, EntityAssociation>, ICommand {

        public GetAssociatedEntities(IRepository<EntityAssociation> repository, IHttpContext context)
            : base(repository, context) {
        }

        public Guid EntityId { get; set; }

        public IQueryable<EntityAssociation> Query() {

            if (this.Id == Guid.Empty) {
                return (from ea in repository.All()
                        where ea.EntityId == this.EntityId
                        select ea);
            } else {
                return (from ea in repository.All()
                        where ea.Id == this.Id
                        select ea);
            }
        }

        public void Execute() {

            this.data = this.Query().ToList();

        }
    }
}
