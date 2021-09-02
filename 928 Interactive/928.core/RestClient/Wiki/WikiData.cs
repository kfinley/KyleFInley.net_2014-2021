using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace _928.Core.RestClient.Wiki {
    public class WikiData {

        
        private XDocument document;

        //private string pageId;
        //private string title;

        public WikiData() {

        }


        public string PageId { get; set; }
        public string Title { get; set; }
        public XDocument Document { get; set; }
        public string WikiText { get; set; }

        //public string PageId {
        //    get {
        //        try {
        //            return this.Query.Pages.Page.PageId;
        //        } catch (NullReferenceException) {
        //            return pageId;
        //        }
        //    }
        //    set {
        //        this.pageId = value;
        //    }
        //}

        //public string Title {
        //    get {
        //        try {
        //            return this.Query.Pages.Page.Title;
        //        } catch (NullReferenceException) {
        //            return this.title;
        //        }
        //    }
        //    set {
        //        this.title = value;
        //    }
        //}
        //public XmlDocument Document {
        //    get {
        //        try {
        //            return this.Query.Pages.Page.Revisions.Rev.Parsetree;
        //        } catch (NullReferenceException) {
        //            return this.document;
        //        }
        //    }
        //    set {
        //        this.document = value;
        //    }
        //}

        //[DeserializeAs(Name = "query")]
        //public Query Query { get; set; }
        
        /*
         * 
         * <?xml version="1.0"?>
         * <api>
         *     <query>
         *         <pages>
         *             <page pageid="594504" ns="0" title="Death Cab for Cutie">
         *                 <revisions>
         *                     <rev parsetree
         *   
         *                     </rev>
         *                 </revisions>
         *             </page>
         *         </pages>
         *     </query>
         * </api>
         * 
         */
    }

    //public class Query {

    //    [DeserializeAs(Name = "pages")]
    //    public Pages Pages { get; set; }
    //}

    //public class Pages {
    //    [DeserializeAs(Name = "page")]
    //    public Page Page { get; set; }
    //}

    //public class Page {

    //    [DeserializeAs(Name = "pageid", Attribute = true)]
    //    public string PageId { get; set; }
    //    [DeserializeAs(Name = "title", Attribute = true)]
    //    public string Title { get; set; }
    //    [DeserializeAs(Name = "revisions")]
    //    public Revisions Revisions { get; set; }
    //}

    //public class Revisions {
    //    [DeserializeAs(Name = "rev")]
    //    public Rev Rev { get; set; }
    //}

    //public class Rev {
    //    [DeserializeAs(Name = "parsetree", Attribute = true)]
    //    public string Parsetree { get; set; }
    //}
}
