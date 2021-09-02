using _928.Entities;
using _928.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KyleFinley.Web.Models {

    public class SiteViewData : BaseViewData {
        
    }

    public class SiteViewData<T> : BaseViewData<T> 
        where T : Entity {
       
    }
}