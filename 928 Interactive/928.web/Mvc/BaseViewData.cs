using System;

using _928.Security;
using _928.Core.Interfaces;
using _928.Entities.Models;

namespace _928.Web.Mvc {


    public abstract class BaseViewData<T> : BaseViewData 
        where T : IEntity {
        
        private Page<T> page;

        public Page<T> Page {
            get {

                if (page == null) {
                    page = new Page<T>();
                }

                return page;
            }
            set {
                this.page = value;
                base.Id = page.Id;
                this.Title = value.Title;
                this.Description = value.Description;
                this.PageImage = value.PageImage;
                //this.Canonical = value.Path;
                this.PublishedDate = value.PublishedDate;
                this.ModifiedDate = value.LastModified;
                this.Description = value.Description;

            }
        }

        public new Guid Id {
            get {
                return this.Page.Id;
            }
        }

      }

    public abstract class BaseViewData {

        private string canonical = string.Empty;

        private bool analyticsTracking = true;

        public IUser CurrentUser { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public string TwitterHandle { get; set; }

        public string Site { get; set; }

        public string Canonical {
            get {
                return this.canonical;
            }
            set {
                //if (value.HasValue()) {
                    if (value.StartsWith("/"))
                        this.canonical = value;
                    else
                        this.canonical = "/" + value;
                //}
            }
        }

        public virtual bool NoIndex { get; set; }

        public bool AnalyticsTracking {
            get {
                return this.analyticsTracking;
            }
            set {
                this.analyticsTracking = value;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PageImage { get; set; }

        public string HeaderImage { get; set; }

        public DateTime? PublishedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
