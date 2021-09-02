using _928.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Entities {

    public class Entity : EntityBase, IEntity {

        //TODO: Add Required Back when KyleFinley.net DB gets updated.
        [Required]
        [StringLength(100, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Name { get; set; }

        [Required]
        public virtual int EntityType { get; set; }

        //public virtual bool Enabled { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        //public virtual Guid TypeId { get; set; }
    }
}
