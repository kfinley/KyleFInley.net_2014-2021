using _928.Core.Data;
using _928.Core.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _928.Core;

namespace _928.Data.EntityFramework {
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
                try {
                    context.SaveChanges();
                } catch (Exception ex) {
                    var handler = new ExceptionHandler(new ExceptionHandlingPolicy(
                        new PolicyInfo {
                            OriginalException = ex.GetType(),
                            Behavior = ExceptionPolicyBehavior.Wrap,
                            NewException = typeof(Exception),
                            Message = "Error calling EFUnitOfWork.Commit(). Message: {0}".FormatWith(ex.Message)
                        }));

                    handler.HandleException(ex);
                }

            }
        }

        public void Dispose() {
            context.Dispose();
        }
    }
}
