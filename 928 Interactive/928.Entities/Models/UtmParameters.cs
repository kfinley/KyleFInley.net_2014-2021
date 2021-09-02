using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _928.Models {
    public struct UtmParameters {
        public static string TwitterShare = "utm_source=twitter.com&utm_medium=social&utm_content={0}&utm_campaign=website-share";
        public static string GoogleShare = "utm_source=plus.google.com&utm_medium=social&utm_content={0}&utm_campaign=website-share";
        public static string LinkedInShare = "utm_source=LinkedIn&utm_medium=social&utm_content={0}&utm_campaign=website-share";
        public static string FacebookShare = "utm_source=facebook.com&utm_medium=social&utm_content={0}&utm_campaign=website-share";
        public static string EmailShare = "utm_source=website&utm_medium=email&utm_content={0}&utm_campaign=website-share";
    }
}
