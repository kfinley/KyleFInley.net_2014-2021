
using System;
using System.ComponentModel.DataAnnotations.Schema;

using _928.Core;
using _928.Core.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace _928.Entities.Models {
    
    public class Page : Url { //: PageBase {

        [Required]
        [StringLength(60, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(160, ErrorMessage = "The {0} can be no longer than {1} characters.")]
        public string Description { get; set; }

        public string PageImage { get; set; }

        public string Content { get; set; }
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime LastModified { get; set; }

        public bool Enabled { get; set; }

        private string twitterShareUrl;
        private string googleShareUrl;
        private string facebookShareUrl;
        private string linkedInShareUrl;
        private string pinterestShareUrl;
        private string emailShareUrl;

        //public string Author { get; set; }

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

        public IShortUrl TwitterShareShortUrl { get; set; }
        public IShortUrl GoogleShareShortUrl { get; set; }
        public IShortUrl FacebookShareShortUrl { get; set; }
        public IShortUrl LinkedInShareShortUrl { get; set; }
        public IShortUrl PinterestShareShortUrl { get; set; }
        public IShortUrl EmailShareShortUrl { get; set; }

        public IUrlClicks TwitterShareUrlClicks { get; set; }
        public IUrlClicks GoogleShareUrlClicks { get; set; }
        public IUrlClicks FacebookShareUrlClicks { get; set; }
        public IUrlClicks LinkedInShareUrlClicks { get; set; }
        public IUrlClicks PinterestShareUrlClicks { get; set; }
        public IUrlClicks EmailShareUrlClicks { get; set; }
    }

    [NotMapped]
    public class Page<T> : Page 
        where T : IEntity {

        public T Entity { get; set; }

    }
}
