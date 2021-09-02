using _928.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace _928.Web.Mvc {
    public class AuthorViewData<T> : BaseViewData<T>
        where T : IEntity {
      
        [Required]
        [StringLength(50, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Url { get; set; }
        public AuthorMode AuthorMode { get; set; }
        public bool Saved { get; set; }
        public string PageType { get; set; }
        
    }
}