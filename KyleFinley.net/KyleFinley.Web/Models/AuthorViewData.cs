using _928.Entities;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class AuthorViewData<T> : SiteViewData
        where T : Entity {
        
        public T Entity { get; set; }
      
        [Required]
        [StringLength(50, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Url { get; set; }
        public AuthorMode AuthorMode { get; set; }
        public bool Saved { get; set; }
        public string PageType { get; set; }
        
    }
}