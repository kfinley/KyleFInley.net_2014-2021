using _928.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Models {
    public class Redirect : EntityBase {

        public bool Do { get; set; }
        public string OldPath { get; set; }
        public string NewPath { get; set; }
        
    }
}
