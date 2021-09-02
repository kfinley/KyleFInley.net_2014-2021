using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class ManageAccountViewData : SiteViewData {
        public bool HasLocalPassword { get; set; }
        public string StatusMessage { get; set; }

        public string ReturnUrl { get; set; }
    }
}