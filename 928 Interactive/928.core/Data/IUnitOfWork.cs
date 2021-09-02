using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Core.Data {
    public interface IUnitOfWork : IDisposable {
        void Commit();
    }
}
