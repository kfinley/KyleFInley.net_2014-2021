
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using _928.UrlShortener;

namespace KyleFinley.Web.Models {

    public class ShortUrlAnalyticsViewData<T> : SiteViewData<ShortUrlAnalytics> {
        public ShortUrlAnalytics Analytics {
            get {
                return base.Page.Entity;
            }
        }
    }
}