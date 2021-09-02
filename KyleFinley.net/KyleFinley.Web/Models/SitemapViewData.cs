using System.Collections.Generic;

using KyleFinley.Models;
using _928.Entities;
using _928.Entities.Models;

namespace KyleFinley.Web.Models {

    public class SitemapViewData : SiteViewData {
        public IList<Page> Urls { get; set; }
    }
}