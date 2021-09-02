using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class ArticlesViewData : BaseSiteViewData {
        public IList<Article> Articles { get; set; }
    }
}