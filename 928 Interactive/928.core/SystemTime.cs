using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _928.Core
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
