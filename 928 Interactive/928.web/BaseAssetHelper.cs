using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace _928.Web
{
    public abstract class BaseAssetHelper
    {

        public static void LoadHashes(string assetDirectory)
        {
            CacheBuster.ProcessDirectory(assetDirectory);
        }

        //public static string Jquery
        //{
        //    get
        //    {
        //        var protocol = "http{0}".FormatWith((AppSettings.Environment == AppSettings.Values.Production) ? "s" : string.Empty);
                
        //        return "<script type='text/javascript' src='{0}://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js'></script>".FormatWith(protocol);
        //    }
        //}
    }
}