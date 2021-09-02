using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class ArticleViewData : BaseSiteViewData {
        public Article Article { get; set; }
    }
}