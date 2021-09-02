using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections;
using _928.Core.MapperHelper;
using _928.Core.Wrappers;
using StructureMap;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace _928.Core {
    public static class Extensions {
  
        #region strings

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args) {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(provider, format, args);
        }

        public static bool HasValue(this string target) {
            return !string.IsNullOrEmpty(target);
        }

        private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);

        public static string Hash(this string target) {
            Check.Argument.IsNotEmpty(target, "target");

            using (MD5 md5 = MD5.Create()) {
                byte[] data = Encoding.Unicode.GetBytes(target);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args) {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target) {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target) {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        public static bool IsEmpty(this string target) {
            if (string.IsNullOrEmpty(target))
                return true;

            return false;
        }

        public static string NullIfEmpty(this string target) {
            if (target.IsEmpty())
                return null;
            else
                return target;
        }
        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A string.</returns>
        public static string TitleCase(this string value) {
            return TitleCase(value, true);
        }

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="ignoreShortWords">If true, does not capitalize words like
        /// "a", "is", "the", etc.</param>
        /// <returns>A string.</returns>
        public static string TitleCase(this string value, bool ignoreShortWords) {
            List<string> ignoreWords = null;
            if (ignoreShortWords) {
                ignoreWords = new List<string>();
                ignoreWords.Add("a");
                ignoreWords.Add("is");
                ignoreWords.Add("was");
                ignoreWords.Add("the");
            }

            string[] tokens = value.Split(' ');
            StringBuilder sb = new StringBuilder(value.Length);
            foreach (string s in tokens) {
                if (ignoreShortWords
                    && s != tokens[0]
                    && ignoreWords.Contains(s.ToLower())) {
                    sb.Append(s + " ");
                } else {
                    sb.Append(s[0].ToString().ToUpper());
                    sb.Append(s.Substring(1).ToLower());
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }

        public static string ToVanityUrl(this string target) {

            return target.ToLower()
                .Replace("†", string.Empty)
                .Replace("\"", string.Empty)
                .Replace("'", string.Empty)
                .Replace("&", "and")
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("!", string.Empty) 
                .Replace("¡", string.Empty)
                .Replace("½", string.Empty)
                .Replace("ü", "u")
                .Replace("ö", "o")
                .Replace("ø", "o")
                .Trim()
                .Replace(' ', '-');
            
        }

        public static string ToStringList(this IEnumerable<string> list, bool commaSeparated) {
            var result = string.Empty;

            foreach (var item in list) {
                result += item.ToString();

                if (list.Last().Equals(item) == false)
                    if (commaSeparated)
                        result += ", ";
                    else
                        result += " ";
            }
            return result;
        }

        public static string ToStringList(this IEnumerable<string> list) {
            return list.ToStringList(true);
        }

        public static string Truncate(this string s, int length, bool atWord, bool addEllipsis) {
            // Return if the string is less than or equal to the truncation length
            if (s == null || s.Length <= length)
                return s;

            // Do a simple tuncation at the desired length
            string s2 = s.Substring(0, length);

            // Truncate the string at the word
            if (atWord) {
                // List of characters that denote the start or a new word (add to or remove more as necessary)
                List<char> alternativeCutOffs = new List<char>() { ' ', ',', '.', '?', '/', ':', ';', '\'', '\"', '\'', '-' };

                // Get the index of the last space in the truncated string
                int lastSpace = s2.LastIndexOf(' ');

                // If the last space index isn't -1 and also the next character in the original
                // string isn't contained in the alternativeCutOffs List (which means the previous
                // truncation actually truncated at the end of a word),then shorten string to the last space
                if (lastSpace != -1 && (s.Length >= length + 1 && !alternativeCutOffs.Contains(s.ToCharArray()[length])))
                    s2 = s2.Remove(lastSpace);
            }

            // Add Ellipsis if desired
            if (addEllipsis)
                s2 += "[...]";

            return s2;
        }

        public static string ScrubUrl(this string target) {
           target = target.Replace("http://", string.Empty).Replace("https://", string.Empty);

           if (target.EndsWith("/")) {
               target = target.Substring(0, target.Length - 1);
           }
           return target;
        }

        #endregion

        #region Collections

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
            return (collection == null) || (collection.Count == 0);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) {
            foreach (var item in items)
                action(item);
        }

        public static T PickRandom<T>(this IEnumerable<T> source) {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static void RemoveKeyIfPresent(this Dictionary<string, string> target, string key) {
            if (target.ContainsKey(key))
                target.Remove(key);
        }

        #endregion

        #region AnonymousTypes

        public static T TolerantCast<T>(this object o, T example) where T : class {
            IComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
            //Get constructor with lowest number of parameters and its parameters 
            var constructor = typeof(T).GetConstructors(
               BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            ).OrderBy(c => c.GetParameters().Length).First();
            var parameters = constructor.GetParameters();

            //Get properties of input object
            var sourceProperties = new List<PropertyInfo>(o.GetType().GetProperties());

            if (parameters.Length > 0) {
                var values = new object[parameters.Length];
                for (int i = 0; i < values.Length; i++) {
                    Type t = parameters[i].ParameterType;
                    //See if the current parameter is found as a property in the input object
                    var source = sourceProperties.Find(delegate(PropertyInfo item) {
                        return comparer.Compare(item.Name, parameters[i].Name) == 0;
                    });

                    //See if the property is found, is readable, and is not indexed
                    if (source != null && source.CanRead &&
                       source.GetIndexParameters().Length == 0) {
                        //See if the types match.
                        if (source.PropertyType == t) {
                            //Get the value from the property in the input object and save it for use
                            //in the constructor call.
                            values[i] = source.GetValue(o, null);
                            continue;
                        } else {
                            //See if the property value from the input object can be converted to
                            //the parameter type
                            try {
                                values[i] = Convert.ChangeType(source.GetValue(o, null), t);
                                continue;
                            } catch {
                                //Impossible. Forget it then.
                            }
                        }
                    }
                    //If something went wrong (i.e. property not found, or property isn't 
                    //converted/copied), get a default value.
                    values[i] = t.IsValueType ? Activator.CreateInstance(t) : null;
                }
                //Call the constructor with the collected values and return it.
                return (T)constructor.Invoke(values);
            }
            //Call the constructor without parameters and return the it.
            return (T)constructor.Invoke(null);


        }

        public static object HydrateAnonymousTypeFromJson(this string json) {
            string test = json;

            return null;

        }

        public static T Deserialize<T>(this string json, T targetType) {
            object result = JsonConvert.DeserializeObject<T>(json);

            return (T)result;
        }

        public static T ToType<T>(this object o, T typeToCastTo) {
            return (T)o;
        }


        #endregion

        #region DateTime

        //TODO:  Externalize this to either config or the DB
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target) {
            return (target >= MinDate) && (target <= MaxDate);
        }

        public static string ToUrlDate(this DateTime target) {
            return "{0}-{1}-{2}".FormatWith(target.Year, target.Month, target.Day);
        }

        #endregion

        #region Enums

        public static IDictionary ToDictionary<TEnumValueType>(this Enum e) {

            if (typeof(TEnumValueType).FullName != Enum.GetUnderlyingType(e.GetType()).FullName) throw new ArgumentException("Invalid type specified.");

            return Enum.GetValues(e.GetType())
                        .Cast<object>()
                        .ToDictionary(key => Enum.GetName(e.GetType(), key),
                                      value => (TEnumValueType)value);
        }

        public static Dictionary<int, string> ToDictionary(this Enum @enum) {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => (int)e, e => Enum.GetName(type, e));
        }

        public static Dictionary<string, TEnum> ToDictionary<TEnum>() where TEnum : struct {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().
                    ToDictionary(e => Enum.GetName(typeof(TEnum), e));
        }

        public static string ToStringList<TEnum>(this IEnumerable<TEnum> list, bool commaSeparated) where TEnum : struct {
            var result = string.Empty;

            foreach (var item in list) {
                result += item.ToString();

                if (list.Last<TEnum>().Equals(item) == false)
                    if (commaSeparated)
                        result += ", ";
                    else
                        result += " ";
            }
            return result;
        }

        public static string ToStringList<TEnum>(this IEnumerable<TEnum> list) where TEnum : struct {
            return list.ToStringList(false);
        }

        #endregion

        #region Exceptions

        public static string GetAllMessages(this Exception ex) {
            if (ex == null)
                throw new ArgumentNullException("ex");

            StringBuilder sb = new StringBuilder();

            while (ex != null) {
                if (!string.IsNullOrEmpty(ex.Message)) {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    sb.Append(ex.Message);
                }

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
        
        public static bool AnySource(this Exception ex, string source) {
             if (ex == null)
                throw new ArgumentNullException("ex");
            if (source.IsEmpty())
                throw new ArgumentNullException("source");

             while (ex != null) {
                 if (ex.Source == source) 
                     return true;
                 ex = ex.InnerException;
             }
             return false;

        }

        #endregion

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : IMappable
            where TDestination : IMappable {
            //var mapper = ObjectFactory.GetInstance<IMapper>();

            //return mapper.Map<TSource, TDestination>(source, destination);

            return MyMapper.Map<TSource, TDestination>(source, destination);

        }

        public static string ToXml(this object source) {

            //Check is object is serializable before trying to serialize
            if (source.GetType().IsSerializable) {
                using (var stream = new MemoryStream()) {
                    var serializer = new XmlSerializer(source.GetType());
                    serializer.Serialize(stream, source);
                    var bytes = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(bytes, 0, bytes.Length);

                    return Encoding.UTF8.GetString(bytes);
                }
            }
            throw new NotSupportedException(string.Format("{0} is not serializable.", source.GetType()));
        }

        public static T FromXml<T>(this string sourceXml) {

            var xDocument = XDocument.Parse(sourceXml);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = xDocument.CreateReader())
                return (T)xmlSerializer.Deserialize(reader);
        }

        public static void SerializeParams<T>(this XDocument doc, List<T> paramList) {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(paramList.GetType());

            System.Xml.XmlWriter writer = doc.CreateWriter();

            serializer.Serialize(writer, paramList);

            writer.Close();
        }

        public static List<T> DeserializeParams<T>(this XDocument doc) {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));

            System.Xml.XmlReader reader = doc.CreateReader();

            List<T> result = (List<T>)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        public static string GetPropertyName<T>(this object target, Expression<Func<T>> propertyLambda) {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null) {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        public static bool IsNull(this object target) {
            return target == null;
        }
        public static bool IsNotNull(this object target) {
            return target != null;
        }

        static bool IsGenericEnumerable(this Type t) {
            var genArgs = t.GetGenericArguments();
            if (genArgs.Length == 1 &&
                    typeof(IEnumerable<>).MakeGenericType(genArgs).IsAssignableFrom(t))
                return true;
            else
                return t.BaseType != null && IsGenericEnumerable(t.BaseType);
        }

    }
}
