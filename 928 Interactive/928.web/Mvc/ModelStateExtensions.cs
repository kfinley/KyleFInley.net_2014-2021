
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _928.Web.Mvc
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Converts the <paramref name="modelState"/> to a List that can be easily serialized.
        /// </summary>
        public static IList<object> ToSerializableList(this ModelStateDictionary modelState)
        {
            var result = new List<object>();

            modelState.Where(x => x.Value.Errors.Any())
                .ToList()
                .ForEach(kvp => result.Add(new
                {
                    Field = kvp.Key,
                    Errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                }));

            return result;
        }
    }
}
