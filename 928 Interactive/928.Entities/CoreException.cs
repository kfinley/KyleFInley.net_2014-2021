using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Entities {
    public class CoreException : EntityBase {

        public DateTime DateCreated { get; set; }

        [Required]
        public string ExceptionType { get; set; }
        [Required]
        public virtual string ExceptionMessage { get; set; }

        public string AdditionalData { get; set; }

    }
}
