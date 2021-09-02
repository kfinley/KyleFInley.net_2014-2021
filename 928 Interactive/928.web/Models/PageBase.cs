using _928.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  _928.Web.Models {
   
    public abstract class PageBase : Entity {

        public PageBase()
            : base() {

        }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(160, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Description { get; set; }
        
        public string PageImage { get; set; }

        public string Content { get; set; }
        public Guid CreatedBy { get; set; }
            
        //public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        
        public DateTime LastModified { get; set; }

    }
}
