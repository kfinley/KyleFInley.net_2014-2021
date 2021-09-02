using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using _928.Core.Interfaces;
using KyleFinley.Data;
using KyleFinley.Commands;

namespace KyleFinley.Tests {
    [TestClass]
    public class StructureMapTests {
        [TestMethod]
        public void Test() {
            var container = new Container(x => {
                //x.For<IHttpContext>().Use<_928.Web.HttpContextWrapper>();
                x.AddRegistry<DataRegistry>();
                //x.AddRegistry<CommandsRegistry>();
            });

            container.AssertConfigurationIsValid();
        }
    }
}
