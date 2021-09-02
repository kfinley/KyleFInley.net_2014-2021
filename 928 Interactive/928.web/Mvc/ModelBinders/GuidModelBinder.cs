using System;
using System.Web.Mvc;

namespace _928.Web.Mvc.ModelBinders {

    public class GuidModelBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var parameter = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (parameter != null)
                return Guid.Parse(parameter.AttemptedValue);
            else
                return Guid.Empty;
        }
    }
}
