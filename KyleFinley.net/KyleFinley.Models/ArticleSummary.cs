
using _928.Core;
using _928.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyleFinley.Models {
    public class ArticleSummary : EntityBase {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentSnippet { get; set; }
        public DateTime PublishedDate { get; set; }
        public Url Url { get; set; }

        public static string TrimContent(string content, int length)
        {

            var matches = Regex.Matches(content, "<p.*?(?=</p>)", RegexOptions.Singleline);

            if (matches.Count > 0)
            {

                var summary = matches[0].Groups[0].Value + "</p>" + matches[1].Groups[0].Value + "</p>";

                var lastTag = summary.Substring(summary.LastIndexOf("<"));

                if (lastTag.IndexOf("/>") == -1)
                {
                    summary = summary.Replace(lastTag, "");
                }

                return summary.Length <= length ? summary : summary.Truncate(length, true, true) + "</p>";

            }
            else {
                return string.Empty;
            }
        }
    }
}
