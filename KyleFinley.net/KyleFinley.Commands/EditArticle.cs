using _928.Commands;
using _928.Core;
using _928.Core.ExceptionHandling;
using _928.Core.Interfaces;
using _928.Data.Repository;
using KyleFinley.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyleFinley.Commands {
    public class EditArticle : BaseDataSourcedCommand<Article>, ICommand {

        public EditArticle(IRepository<Article> repository, IHttpContext context)
            : base(repository, context) {
        }

        public void Execute() {

            try {
                this.data.LastModified = DateTime.Now;
                if (this.data.PublishedDate == DateTime.MinValue && this.data.Enabled == true) {
                    this.data.PublishedDate = DateTime.Now;
                }

                if (this.data.Author.IsEmpty()) {
                    this.data.Author = "Kyle Finley";
                }

                if (this.data.Enabled) {
                    var createShortUrls = CommandFactory.Create<CreatePageShortUrls>();
                    createShortUrls.Data = this.Data;
                    dispatcher.Run(createShortUrls, false);
                }

                repository.Update(this.data);
                repository.UnitOfWork.Commit();
            } catch (Exception ex) {
                throw new Exception("Error updating Article. Message: {0}".FormatWith(ex.Message), ex);
            }
        }

        public string Site { get; set; }
        public string Url { get; set; }
    }
}
