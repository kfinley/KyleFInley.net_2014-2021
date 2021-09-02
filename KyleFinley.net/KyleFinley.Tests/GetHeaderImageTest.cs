using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KyleFinley.Web.App_Start;
using _928.Commands;
using KyleFinley.Commands;
using KyleFinley.Models;
using _928.Web;

namespace KyleFinley.Tests {
    [TestClass]
    public class GetHeaderImageTest {
        [TestMethod]
        public void Test() {
            IocBootstrapper.Run();

            var getHeaderImage = CommandFactory.Create<GetHeaderImage>();
            getHeaderImage.Type = HeaderImageType.Large;

            var dispatcher = new CommandDispatcher(new HttpContextWrapper());
            dispatcher.Run(getHeaderImage);

            var result = getHeaderImage.Data;

        }
    }
}
