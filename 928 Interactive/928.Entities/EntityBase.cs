using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _928.Core.MapperHelper;
using System.ComponentModel.DataAnnotations;
using _928.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace _928.Entities
{
    public abstract class ModelBase : IMappable {
        
        public void ValidateData() {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject(this, context, results, true) == false) {
                var errors = new StringBuilder();
                foreach (var error in results) {

                    errors.AppendLine(error.ErrorMessage);
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Validation Error: {0}".FormatWith(error.ErrorMessage));
#endif
                }
                throw new ValidationException("Validation Error: {0}".FormatWith(errors.ToString()));
            }
        }
    }

    public abstract class EntityBase : ModelBase, IEntityBase
    {
        public virtual Guid Id { get; set; }
        //public virtual int EntityType { get; set; }

    }

    public interface IEntityBase {
        Guid Id { get; set; }
    }
}
