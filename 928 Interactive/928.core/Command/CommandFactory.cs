using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace _928.Core {
    public class CommandFactory {
        internal static IContainer Container;
        private static CommandFactory commandFactoryInstance;

        public static void Initialize(IContainer container) {
            commandFactoryInstance = new CommandFactory(container);
            Container = container;
        }

        public CommandFactory(IContainer container) {
            Container = container;
        }

        public static T Create<T>() {
            return Container.GetInstance<T>();
        }

        public static ICommand Create(Type command, params Tuple<string, object>[] args) {

            switch (args.Count()) {
                case 1:
                    return (ICommand)Container.With(args[0].Item1).EqualTo(args[0].Item2).GetInstance(command);
                case 2:
                    return (ICommand)Container.With(args[0].Item1).EqualTo(args[0].Item2)
                                                .With(args[1].Item1).EqualTo(args[1].Item2)
                                                .GetInstance(command);
                case 3:
                    return (ICommand)Container.With(args[0].Item1).EqualTo(args[0].Item2)
                                                .With(args[1].Item1).EqualTo(args[1].Item2)
                                                .With(args[2].Item1).EqualTo(args[2].Item2)
                                                .GetInstance(command);
                default:
                    throw new Exception("Not able to create a command with {0} arguments.  Check CommandFactory.Create() to resolve.".FormatWith(args.Count()));

            }
        }
    }
}
