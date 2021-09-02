using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Data.EntityFramework {
    public class UnmappedTypesConvention : Convention {
        public UnmappedTypesConvention() {
            
            var config = Types()
                 .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeIgnoreConfiguration<>));
            
            config.Configure(c => c.Ignore());

        }
    }
}
