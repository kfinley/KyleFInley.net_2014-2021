using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using _928.Entities.Models;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class EditPage<T> : BaseDataSourcedCommand<T>, ICommand 
        where T : Page {

        public EditPage(IRepository<T> repository, IHttpContext httpContext)
            : base(repository, httpContext) {
        }

        public void Execute() {

            try {

                this.data.LastModified = DateTime.Now;
                if (this.data.PublishedDate.IsNull() && this.data.Enabled == true) {
                    this.data.PublishedDate = DateTime.Now;
                }

                if (this.data.Author.IsEmpty()) {
                    this.data.Author = "Kyle Finley";
                }

                if (this.data.Enabled) {
                    var createShortUrls = CommandFactory.Create<CreatePageShortUrls>();
                    createShortUrls.Site = this.Site;
                    createShortUrls.Url = this.Url;
                    createShortUrls.Data = this.data;
                    dispatcher.Run(createShortUrls, false);
                }

                repository.Update(this.data);
                repository.UnitOfWork.Commit();


                // update page in cache
                this.HttpContext.Cache[Id.ToString()] = this.data;

            } catch (Exception ex) {

                throw new Exception("Error updating {0} Message: {1}".FormatWith(this.data != null ? this.data.GetType().Name : "Entity", ex.Message), ex);
            }
        }

        public string Site { get; set; }
        public string Url { get; set; }
    }
}
