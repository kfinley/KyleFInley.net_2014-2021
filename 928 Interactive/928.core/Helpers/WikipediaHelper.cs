using _928.Core.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _928.Core.Helpers {
    public static class WikipediaHelper {
        public static string WikiScrub(this string target) {
            if (string.IsNullOrEmpty(target) == false)
                return target.ScrubInternal();
            else
                return target;
        }

        private static string ScrubInternal(this string target, bool takeFirstDuplicate = false) {
            return target
                .RemoveWikiDuplicates(takeFirstDuplicate)
                .RemoveWikiComments()
                .RemoveWikiCitation()
                .RemoveInlineWikiLinks()
                .Replace(":-bass", string.Empty)
                .Replace(":-vocals", string.Empty)
                .Replace(":-guitar", string.Empty)
                .Replace(":-drums", string.Empty)
                .Replace("- Guitar", string.Empty)
                .Replace("[[", string.Empty)
                .Replace("]]", string.Empty)
                .Replace("&nbsp;", " ")
                .Replace("&ndash;", "-")
                .Replace("<small>", string.Empty)
                .Replace("</small>", string.Empty)
                .Replace("*", string.Empty)
                .Replace("'''", string.Empty)
                .Trim();
        }

        private static string RemoveWikiDuplicates(this string target, bool takeFirstDuplicate = false) {

            return Regex.Replace(target, @"\[\[(.+?)\]\]",
                                    delegate(Match match) {
                                        string m = match.ToString();
                                        if (m.Contains('|')) {
                                            if (takeFirstDuplicate)
                                                return m.Split('|')[0] + "]]";
                                            else
                                                return m.Split('|')[1] + "]]";
                                        } else
                                            return m;
                                    });
        }

        public static string RemoveWikiComments(this string target) {
            return Regex.Replace(target, @"(<!--)(.*?)(\-->)", String.Empty, RegexOptions.Multiline);
        }

        public static string RemoveWikiCitation(this string target) {

            return Regex.Replace(target, "<ref(.*?)</ref>|<ref(.*?)/>", String.Empty, RegexOptions.Multiline);

        }

        public static string RemoveParentheses(this string target) {

            return Regex.Replace(target, @"\(              # Match an opening parenthesis.
                                          (?>             # Then either match (possessively):
                                           [^()]+         #  any characters except parentheses
                                          |               # or
                                           \( (?<Depth>)  #  an opening paren (and increase the parens counter)
                                          |               # or
                                           \) (?<-Depth>) #  a closing paren (and decrease the parens counter).
                                          )*              # Repeat as needed.
                                         (?(Depth)(?!))   # Assert that the parens counter is at zero.
                                         \)               # Then match a closing parenthesis.",
                                    string.Empty, RegexOptions.IgnorePatternWhitespace);
        }

        public static string RemoveInlineWikiLinks(this string target) {

            return Regex.Replace(target, @"\(              # Match an opening parenthesis.
                                        (?>             # Then either match (possessively):
                                        [^()]+         #  any characters except parentheses
                                        |               # or
                                        \( (?<Depth>)  #  an opening paren (and increase the parens counter)
                                        |               # or
                                        \) (?<-Depth>) #  a closing paren (and decrease the parens counter).
                                        )*              # Repeat as needed.
                                        (?(Depth)(?!))   # Assert that the parens counter is at zero.
                                        \)               # Then match a closing parenthesis.",
                                        delegate(Match match) {
                                            string t = match.ToString();
                                            if (t.Contains('|'))
                                                return t.Substring(t.IndexOf('|') + 1);
                                            else
                                                return t;
                                        });
        }

        public static string RemoveWikiIpa(this string target) {

            return Regex.Replace(target, @"\(IPA([^)]*)\)",
                delegate(Match match) {
                    var m = match.ToString();
                    if (m.ToLower().Contains("born"))
                        return "(" + m.Substring(m.ToLower().IndexOf("born"));
                    else
                        return string.Empty;
                });
        }

        public static string RemoveBreaks(this string target) {
            return Regex.Replace(target, "<br/ >|<br/>|<br/>|<br>", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RemoveExtraWordsInParentheses(this string target) {

            if (target.Contains('(') && target.IndexOf('(') != 0) {
                return target.Substring(0, target.IndexOf('(') - 1);
            } else return target;
        }

        public static string GetFirstTextValueFromXmlNode(this string target) {
            var matches = Regex.Matches(target, @"(?<=\[\[)(.*?)(?=\])");
            string result = matches[0].Value;
            return result;
        }

        public static string GetTextValueFromXmlNode(this string target) {

            target = Regex.Replace(target, @"(?<=\{\{)(.*?)(?=\})", string.Empty);
            var matches = Regex.Matches(target, @"(?<=\[\[)(.*?)(?=\])");
            string result = String.Join(", ", matches.Cast<Match>().Select(m => m.Value.Contains('|') ? m.Value.Split('|')[0] : m.Value));
            return result;
        }

        public static string ToWikiUrl(this string target) {
            return target.Trim().Replace(' ', '_');
        }

        private static List<Tuple<string, string>> ToTupleList(List<string> list) {
            list.RemoveAll(x => x == string.Empty);
            var result = new List<Tuple<string, string>>(); ;
            for (int i = 0; i < list.Count(); i++) {

                if (list[i].Contains("See:"))
                    list[i] = list[i].Replace("See:", string.Empty);
                if (list[i].Contains("#"))
                    list[i] = list[i].Remove(list[i].IndexOf('#'));

                list[i] = list[i].ScrubInternal(true);

                if (list[i] != string.Empty) {
                    if (list[i].Contains('/')) {
                        var items = list[i].Split('/');

                        result.Add(new Tuple<string, string>(items[0].RemoveExtraWordsInParentheses(), items[0].ToWikiUrl()));
                        result.Add(new Tuple<string, string>(items[1].RemoveExtraWordsInParentheses(), items[1].ToWikiUrl()));

                    } else {
                        result.Add(new Tuple<string, string>(list[i].RemoveExtraWordsInParentheses(), list[i].ToWikiUrl()));
                    }
                }
            }
            return result;
        }

        public static List<Tuple<string, string>> BreakListToList(string target) {
            if (string.IsNullOrEmpty(target)) {
                return null;
            } else {
                target = Regex.Replace(target, "<br/ >|<br/>|<br/>|<br>", "<br />", RegexOptions.IgnoreCase);

                if (target.StartsWith("<br />"))
                    target = target.Substring(6);

                var list = target.Split(new string[] { "<br />" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return ToTupleList(list);
            }
        }

        public static List<Tuple<string, string>> CommaListToList(string target) {
            if (string.IsNullOrEmpty(target)) {
                return null;
            } else {

                var list = target.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return ToTupleList(list);
            }
        }

        public static List<Tuple<string, string>> NewLineListToList(string target) {
            if (string.IsNullOrEmpty(target)) {
                return null;
            } else {

                var list = target.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                return ToTupleList(list);
            }
        }

        public static string GetSummary(string text) {

            var wikiText = Regex.Replace(text, @" {{
                                                    [^{}]*
                                                    (
                                                        (
                                                            (?<Open>{{)
                                                            [^{}]*
                                                        )+
                                                        (
                                                            (?<Close-Open>}})
                                                            [^{}]*
                                                        )+
                                                    )*
                                                    (?(Open)(?!))
                                                    }}",
                                                    string.Empty, RegexOptions.IgnorePatternWhitespace);

            var result = wikiText.RemoveWikiIpa().ScrubInternal();

            return result;

        }

        private static Dictionary<string, string> SplitToProperties(this string text) {

            text = text.RemoveWikiComments().RemoveWikiCitation().RemoveWikiIpa();
            if (text.StartsWith(" name") == false)
                text = text.Substring(text.IndexOf("\n") + 2);
            return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.StartsWith("*") ? new string[] { "!IGNORE!", string.Empty } : part.Split('='))
                .Where(part => part[0] != "!IGNORE!" &&
                        part.Count() == 1 &&
                        part[0].Trim().ToLower() != string.Empty
                        )
                .ToDictionary(split => split[0].Trim(), split => (split.Count() == 1 ? split[0] : split[1]).RemoveInlineWikiLinks());
        }

        public static IList<T> ConvertToList<T>(Func<IList<Tuple<string, string>>, IList<T>> toList, string originalList) {
            // Remove an URLs in list
            originalList = Regex.Replace(originalList, @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", string.Empty, RegexOptions.Singleline | RegexOptions.Compiled);

            return toList(originalList.ToLower().Contains("<br") ? WikipediaHelper.BreakListToList(originalList)
                        : originalList.Contains("\n") ? WikipediaHelper.NewLineListToList(originalList)
                        : WikipediaHelper.CommaListToList(originalList));
        }
    }
}
