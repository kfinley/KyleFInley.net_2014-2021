using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using StructureMap;
using System.Diagnostics;

namespace _928.Core.Tests
{
    [TestClass]
    public class MultiThreadedCommandsTest
    {
        CommandDispatcher dispatcher = new CommandDispatcher();

        [TestMethod]
        public void CallCommandsAsyncTest()
        {
            var watch = Stopwatch.StartNew();

            CommandFactory.Initialize(ObjectFactory.Container);

            var c1 = CommandFactory.Create<Command1>();
            dispatcher.Run(c1);

            var c2 = CommandFactory.Create<Command2>();
            dispatcher.Run(c2);

            var c3 = CommandFactory.Create<Command3>();
            dispatcher.Run(c3);

            Assert.AreEqual("Command1 Done", c1.Data);
            Assert.AreEqual("Command2 Done", c2.Data);
            Assert.AreEqual("Command3 Done", c3.Data);

            Trace.WriteLine("Milliseconds Elapsed: {0}".FormatWith(watch.ElapsedMilliseconds));
        }
    }

    public class TestClass
    {
        public TestClass()
        {

        }

        public string Result
        {
            get;
            set;
        }
      
    }

    internal class Command1 : BaseCommand<TestClass>, ICommand
    {
        public void Execute()
        {
            Thread.Sleep(10000);
            Data.Result = "Command1 Done";
        }
    }

    internal class Command2 : BaseCommand<TestClass>, ICommand
    {
        public void Execute()
        {
            Thread.Sleep(20000);
            Data.Result = "Command2 Done";
            //throw new InvalidOperationException("Command 2 Failed");
        }
    }

    internal class Command3 : BaseCommand<TestClass>, ICommand
    {
        public void Execute()
        {
            Thread.Sleep(30000);
            Data.Result = "Command3 Done";
        }
    }
}
