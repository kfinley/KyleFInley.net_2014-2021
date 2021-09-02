using _928.Entities;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {
    public class ViewData<T> : SiteViewData<T> {

        public T Entity { get; set; }
    }
}