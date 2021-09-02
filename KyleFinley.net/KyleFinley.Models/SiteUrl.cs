using _928.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Models {
    public class SiteUrl : Url {

        //public string Path { get; set; }
        //public Guid EntityId { get; set; }
        //public EntityType EntityType { get; set; }
        public DateTime? LastModified { get; set; }
       
        //public override EntityType EntityType {
        //    get {
        //        return (Models.EntityType)base.EntityType;
        //    }
        //    set {
        //        base.EntityType = (int)value;
        //    }
        //}

        //public int UrlStructureLevel { get; set; }
    }
}
