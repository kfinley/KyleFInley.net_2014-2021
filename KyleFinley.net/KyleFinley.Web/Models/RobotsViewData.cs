using System.Collections.Generic;

using KyleFinley.Models;
using _928.Entities;

namespace KyleFinley.Web.Models {
    public class RobotsViewData : SiteViewData {
        public IList<Url> Allow { get; set; }
        public IList<Url> Disallow { get; set; }
    }
}