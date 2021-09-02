using _928.Entities;
using _928.Entities.Models;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Data {

    public class PagesMap : EntityTypeConfiguration<Page> {

          public PagesMap() {
            ToTable("Pages");
            HasKey(p => p.Id).Property(p => p.Id);

            Ignore(p => p.CreatedBy);
            Ignore(p => p.Author);

        }

       // public PageMap() {

       //     this.Map(x => {
       //         x.ToTable("Entities");
       //     })
       //     .Map<Home>(x => x.Requires("EntityType").HasValue((int)EntityType.Home))
       //     .Map<Category>(x => x.Requires("EntityType").HasValue((int)EntityType.Category))
       //     .Map<Article>(x => x.Requires("EntityType").HasValue((int)EntityType.Article));
            
       //     Property(p => p.Title).IsRequired().HasMaxLength(60);
       //     Property(p => p.Description).IsRequired().HasMaxLength(160);
       //     Property(p => p.Content).IsRequired();
       //     Property(p => p.Author).IsRequired();
       //     Property(p => p.CreatedDate).IsRequired();
       //     Property(p => p.PublishedDate);
       //     Property(p => p.LastModified).IsRequired();
       //     Property(p => p.Enabled);
       //     Property(p => p.PageImage);
 
       //     Property(p => p.TwitterShareUrl);
       //     Property(p => p.GoogleShareUrl);
       //     Property(p => p.FacebookShareUrl);
       //     Property(p => p.LinkedInShareUrl);
       //     Property(p => p.PinterestShareUrl);
       //     Property(p => p.EmailShareUrl);

       //     Ignore(p => p.TwitterShareUrlClicks);
       //     Ignore(p => p.GoogleShareUrlClicks);
       //     Ignore(p => p.FacebookShareUrlClicks);
       //     Ignore(p => p.LinkedInShareUrlClicks);
       //     Ignore(p => p.EmailShareUrlClicks);
       //     Ignore(p => p.PinterestShareUrlClicks);

       //     Ignore(p => p.TwitterShareShortUrl);
       //     Ignore(p => p.GoogleShareShortUrl);
       //     Ignore(p => p.FacebookShareShortUrl);
       //     Ignore(p => p.LinkedInShareShortUrl);
       //     Ignore(p => p.EmailShareShortUrl);
       //     Ignore(p => p.PinterestShareShortUrl);
       //}
    }

    //public class PageBaseMap : EntityTypeConfiguration<PageBase> {

    //    public PageBaseMap() {

    //        Ignore(p => p.CreatedBy);

    //    }
    //}
}