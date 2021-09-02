using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core
{
    public interface ICommand
    {
        void Execute();

        Task Task { get; set; }
        bool IsAsync { get; set; }
    }
}
