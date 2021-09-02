using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KyleFinley.Models;
using _928.Entities;

namespace KyleFinley.Web.Models {
    public class UrlsViewData : SiteViewData {
        public IList<Url> Urls { get; set; }
    }
}