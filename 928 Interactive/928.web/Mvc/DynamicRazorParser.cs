using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

using _928.Core.Interfaces;

namespace _928.Web.Mvc
{
    public static class DynamicRazorParser
    {
        private static ICache cache = new CachWrapper();

        public static IHtmlString RenderContent(this HtmlHelper htmlHelper, string content)
        {
            if (content.IndexOf("@Html") > -1)
            {
                var finished = false;
                var start = 0;

                while (finished == false)
                {
                    start = content.IndexOf("@Html", start);
                    if (start == -1)
                    {
                        finished = true;
                        continue;
                    }

                    var end = content.IndexOf(')', start);

                    if (end == -1)
                    {
                        finished = true;
                        continue;
                    }

                    var razorFunction = content.Substring(start, end - start + 1);

                    var result = cache[razorFunction];

                    if (result == null)
                    {
                        var function = razorFunction.Substring(razorFunction.IndexOf('.') + 1, razorFunction.IndexOf('(') - razorFunction.IndexOf('.') - 1);
                        string[] functionParams = GetMethodParams(razorFunction);

                        var methodParams = new List<object>();
                        methodParams.Add(htmlHelper);

                        for (int i = 0; i < functionParams.Length; i++)
                        {
                            var item = functionParams[i].Trim();

                            if (item.StartsWith("\""))
                            {
                                methodParams.Add(item.Trim('"'));
                            }
                            else if (item.StartsWith("new"))
                            {
                                methodParams.Add(CreateRouteValueDictionary(item));
                            }
                            else
                            {
                                var methodFullName = item.Split('.');
                                var methodClass = methodFullName[methodFullName.Length - 2];
                                var method = methodFullName[methodFullName.Length - 1];
                                var methodNamespace = item.Substring(0, item.IndexOf(methodClass) - 1);

                                methodParams.Add(BuildManager.GetType($"{methodNamespace}.{methodClass}", true, false).GetField(method).GetValue(null));
                            }
                        }

                        var targetClass = cache[function] as Type;

                        if (targetClass == null)
                        {
                            switch (function)
                            {
                                case "RouteLink":
                                case "ActionLink":
                                    targetClass = BuildManager.GetType("System.Web.Mvc.Html.LinkExtensions", true, false);
                                    break;
                                default:
                                    throw new Exception($"Unknown extension method: {function}.");
                            }
                        }

                        var paramsMatch = new List<Type>();
                        methodParams.ForEach(item =>
                        {
                            paramsMatch.Add(item.GetType());
                        });

                        var targetMethod = cache[function + "_Method"] as MethodInfo;
                        if (targetMethod == null)
                        {
                            targetMethod = targetClass.GetMethod(function, paramsMatch.ToArray());
                            cache[function + "_Method"] = targetMethod;
                        }

                        result = targetMethod.Invoke(htmlHelper, methodParams.ToArray());
                        cache[razorFunction] = result;
                    }
                    content = content.Replace(razorFunction, result.ToString());
                }

                return new HtmlString(content);
            }

            return new HtmlString(content);

        }

        private static string[] GetMethodParams(string razorFunction)
        {
            return razorFunction.Substring(razorFunction.IndexOf('(') + 1, razorFunction.Length - razorFunction.IndexOf('(') - 2).Split(',');
        }

        private static RouteValueDictionary CreateRouteValueDictionary(string anonymous)
        {
            var result = new Dictionary<string, object>();

            var anonymousParts = anonymous.Substring(anonymous.IndexOf('{') + 1, anonymous.Length - anonymous.IndexOf('{') - 2).Split(',');
            foreach (var part in anonymousParts)
            {
                var prop = part.Split('=');
                result.Add(prop[0].Trim(), prop[1].Replace("\"", string.Empty).Trim());
            }
            return new RouteValueDictionary(result); ;
        }
    }
}
