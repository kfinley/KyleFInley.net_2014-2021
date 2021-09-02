using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _928.Core
{
    public class ShortTime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public override string ToString()
        {
            if (Hours > 0)
                return "{0,2}:{1,2}:{2,2}".FormatWith(Hours, Minutes, Seconds);
            else 
                return "{1,2}:{2,2}".FormatWith(Minutes, Seconds);

        }
    }
}