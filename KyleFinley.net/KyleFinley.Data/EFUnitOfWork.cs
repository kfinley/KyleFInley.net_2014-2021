
using _928.Core.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyleFinley.Data {
    public class EFUnitOfWork : IUnitOfWork {
        private readonly IDbContext context;
        private object lockObject = new object();

        public EFUnitOfWork(IDbContext context) {
            lock (lockObject) {
                this.context = context;
            }
        }

        public void Commit() {
            lock (lockObject) {
                context.SaveChanges();
            }
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
