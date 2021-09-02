using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class ListViewData<T> : SiteViewData {
        public IList<T> Items { get; set; }
    }
}