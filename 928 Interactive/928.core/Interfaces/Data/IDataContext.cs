using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Interfaces.Data {
    public interface IDataContext {

        ITransaction BeginTransaction();
    }

    public interface ITransaction : IDisposable {
        void Commit();
    }
}
