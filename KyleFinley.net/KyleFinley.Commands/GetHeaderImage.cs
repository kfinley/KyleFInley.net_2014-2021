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
    public class GetHeaderImage : BaseDataSourcedCommand<Image>, ICommand {

        public GetHeaderImage(IRepository<Image> repository, IHttpContext context)
            : base(repository, context) {
        }

        public HeaderImageType Type { get; set; }

        public void Execute() {

            try {
                var images = new List<string>() { "KyleFinley.net-Lincoln-Boston.jpg", "KyleFinley.net-JM-Curley-Boston.jpg", "KyleFinley.net-Luckys-Boston.jpg" };

                this.data = new Image {
                    FileName = images.PickRandom()
                };
            } catch (Exception ex) {
               throw new Exception("Error retrieving Header Image. Message: {0}".FormatWith(ex.Message), ex);
            }
        }
    }
}
