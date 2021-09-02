using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _928.Entities;
using _928.Data.Repository;
using _928.Core.Interfaces;

namespace _928.Commands {
    public class SaveEntityAssociation : BaseDataSourcedCommand<EntityAssociation>, ICommand {

        public SaveEntityAssociation(IRepository<EntityAssociation> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            if (AssociationExistsOrPending())
                return;

            this.data.DateCreated = DateTime.Now;

            this.data = repository.Insert(this.data);

        }

        private bool AssociationExistsOrPending() {

            var association = (from x in repository.All()
                               where x.Id == this.data.Id &&
                               x.EntityId == this.data.EntityId
                               select x).FirstOrDefault();

            if (association == null) {
                association = (from x in repository.Local(x => x.Id == this.data.Id && x.EntityId == this.Data.EntityId) select x).FirstOrDefault();
            }

            if (association != null) {
                return true;
                //todo: merge any updates with that's been saved so far.
            }

            return false;
        }
    }
}