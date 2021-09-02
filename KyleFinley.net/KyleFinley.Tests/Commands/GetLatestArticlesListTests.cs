using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KyleFinley.Web.App_Start;
using _928.Commands;
using KyleFinley.Commands;
using _928.Web;

namespace KyleFinley.Tests.Commands {
    [TestClass]
    public class GetLatestArticlesListTests {
        [TestMethod]
        public void Test() {
            IocBootstrapper.Run();

            var cmd = CommandFactory.Create<GetLatestArticlesList>();

            // Run Commands
            var dispatcher = new CommandDispatcher(new HttpContextWrapper());
            dispatcher.Run(cmd, false);

            Assert.AreEqual(3, cmd.Data, cmd.Exceptions[0].Message);

        }
    }
}
