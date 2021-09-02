
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _928.Core;
using _928.Entities;
//using _928.UrlShortener;

namespace _928.Web.Models {
    //public interface IPage {

    //    Guid Id { get; set; }
    //    bool Enabled { get; set; }

    //    string Title { get; set; }
    //    string Description { get; set; }
    //    string PageImage { get; set; }
    //    string Content { get; set; }
    //    Guid CreatedBy { get; set; }

    //    DateTime CreatedDate { get; set; }
    //    DateTime? PublishedDate { get; set; }
    //    DateTime LastModified { get; set; }

    //    string TwitterShareUrl { get; set; }
    //    string GoogleShareUrl { get; set; }
    //    string FacebookShareUrl { get; set; }
    //    string LinkedInShareUrl { get; set; }
    //    string PinterestShareUrl { get; set; }
    //    string EmailShareUrl { get; set; }

    //    ShortUrl TwitterShareShortUrl { get; set; }
    //    ShortUrl GoogleShareShortUrl { get; set; }
    //    ShortUrl FacebookShareShortUrl { get; set; }
    //    ShortUrl LinkedInShareShortUrl { get; set; }
    //    ShortUrl PinterestShareShortUrl { get; set; }
    //    ShortUrl EmailShareShortUrl { get; set; }

    //    UrlClicks TwitterShareUrlClicks { get; set; }
    //    UrlClicks GoogleShareUrlClicks { get; set; }
    //    UrlClicks FacebookShareUrlClicks { get; set; }
    //    UrlClicks LinkedInShareUrlClicks { get; set; }
    //    UrlClicks PinterestShareUrlClicks { get; set; }
    //    UrlClicks EmailShareUrlClicks { get; set; }

    //}

    public abstract class Page : PageBase {

        private string twitterShareUrl;
        private string googleShareUrl;
        private string facebookShareUrl;
        private string linkedInShareUrl;
        private string pinterestShareUrl;
        private string emailShareUrl;
        
        public string Author { get; set; }
        
        public string TwitterShareUrl {
            get { return this.twitterShareUrl; }
            set {
                if (this.TwitterShareShortUrl == null && value.HasValue()) {
                    this.TwitterShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.twitterShareUrl = value;
            }
        }
        public string GoogleShareUrl {
            get { return this.googleShareUrl; }
            set {
                if (this.GoogleShareShortUrl == null && value.HasValue()) {
                    this.GoogleShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.googleShareUrl = value;
            }
        }
        public string FacebookShareUrl {
            get { return this.facebookShareUrl; }
            set {
                if (this.FacebookShareShortUrl == null && value.HasValue()) {
                    this.FacebookShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.facebookShareUrl = value;
            }
        }
        public string LinkedInShareUrl {
            get { return this.linkedInShareUrl; }
            set {
                if (this.LinkedInShareShortUrl == null && value.HasValue()) {
                    this.LinkedInShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.linkedInShareUrl = value;
            }
        }
        public string PinterestShareUrl {
            get { return this.pinterestShareUrl; }
            set {
                if (this.PinterestShareShortUrl == null && value.HasValue()) {
                    this.PinterestShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.pinterestShareUrl = value;
            }
        }
        public string EmailShareUrl {
            get { return this.emailShareUrl; }
            set {
                if (this.EmailShareShortUrl == null && value.HasValue()) {
                    this.EmailShareShortUrl = new ShortUrl {
                        ServiceDomain = value.Split('/')[0],
                        Key = value.Split('/')[1]
                    };
                }
                this.emailShareUrl = value;
            }
        }

        public ShortUrl TwitterShareShortUrl { get; set; }
        public ShortUrl GoogleShareShortUrl { get; set; }
        public ShortUrl FacebookShareShortUrl { get; set; }
        public ShortUrl LinkedInShareShortUrl { get; set; }
        public ShortUrl PinterestShareShortUrl { get; set; }
        public ShortUrl EmailShareShortUrl { get; set; }

        public UrlClicks TwitterShareUrlClicks { get; set; }
        public UrlClicks GoogleShareUrlClicks { get; set; }
        public UrlClicks FacebookShareUrlClicks { get; set; }
        public UrlClicks LinkedInShareUrlClicks { get; set; }
        public UrlClicks PinterestShareUrlClicks { get; set; }
        public UrlClicks EmailShareUrlClicks { get; set; }
    }
}
