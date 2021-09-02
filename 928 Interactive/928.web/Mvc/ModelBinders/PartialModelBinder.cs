using _928.Core;
using _928.Core.Attributes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace _928.Web.Mvc.ModelBinders
{
    public class PartialModelBinder<T> : DefaultModelBinder
    {
        public PartialModelBinder()
        {
            this.Validate = ValidateData;
        }

        public Func<ControllerContext, ExpandoObject, T> LoadExistingModel { get; set; }
        public Action<object, ModelStateDictionary> Validate { get; set; }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Check.Argument.IsNotNull(LoadExistingModel, "LoadExisting Func is null in ParialModelBinder.");

            var model = LoadExistingModel(controllerContext, PopulateDynamicModel(controllerContext, bindingContext));

            if (model.IsNotNull())
            {
                Validate(model, bindingContext.ModelState);
                return model;
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        private ExpandoObject PopulateDynamicModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            IDictionary<string, object> underlyingObject = new ExpandoObject();

            foreach (var item in controllerContext.HttpContext.Request.Form.AllKeys)
            {
                SetPropertyValue(underlyingObject, item, item, controllerContext, bindingContext);
            }
            return underlyingObject as ExpandoObject;
        }

        private void SetPropertyValue(IDictionary<string, object> underlyingObject, string currentItem, string originalItem, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var shouldPerformRequestValidation = controllerContext.Controller.ValidateRequest && bindingContext.ModelMetadata.RequestValidationEnabled;

            if (currentItem.Contains("."))
            {
                var childName = currentItem.Substring(0, currentItem.IndexOf('.'));
                var childProperty = currentItem.Substring(currentItem.IndexOf('.') + 1);
                SetNestedProperyValue(underlyingObject, originalItem, childName, childProperty, controllerContext, bindingContext);
            }
            else if (currentItem.Contains("["))
            {
                var childName = currentItem.Substring(0, currentItem.IndexOf('['));
                var childProperty = currentItem.Substring(currentItem.IndexOf('[') + 1, currentItem.IndexOf(']') - (currentItem.IndexOf('[') + 1));

                if (childProperty == string.Empty)
                {
                    var value = GetValue(bindingContext, shouldPerformRequestValidation, originalItem);
                    underlyingObject.Add(childName, value);
                    return;
                }

                SetNestedProperyValue(underlyingObject, originalItem, childName, childProperty, controllerContext, bindingContext);
            }
            else
            {
                var value = GetValue(bindingContext, shouldPerformRequestValidation, originalItem);
                underlyingObject.Add(currentItem, value);
            }
        }

        private void SetNestedProperyValue(IDictionary<string, object> underlyingObject, string originalItem, string childName, string childProperty, ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (underlyingObject.ContainsKey(childName))
            {
                SetPropertyValue((IDictionary<string, object>)underlyingObject[childName], childProperty, originalItem, controllerContext, bindingContext);
            }
            else
            {
                var nestedObject = new ExpandoObject();
                IDictionary<string, object> underlyingChildObject = nestedObject;
                SetPropertyValue(nestedObject, childProperty, originalItem, controllerContext, bindingContext);
                underlyingObject.Add(childName, nestedObject);
            }
        }

        private void ValidateData(object model, ModelStateDictionary modelState)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(model, context, errors, true) == false)
            {
                foreach (var error in errors)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Validation Error: {0}".FormatWith(error.ErrorMessage));
#endif
                    if (error is CompositeValidationResult)
                    {
                        modelState.AddModelError(((CompositeValidationResult)error).Results.First().MemberNames.First(), error.ErrorMessage);
                    }
                    else
                    {
                        modelState.AddModelError(error.MemberNames.First(), error.ErrorMessage);
                    }
                }
            }
        }

        private object GetValue(ModelBindingContext bindingContext, bool performRequestValidation, string key)
        {
            var unvalidatedValueProvider = bindingContext.ValueProvider as IUnvalidatedValueProvider;
            var providerResult = (unvalidatedValueProvider != null)
                                ? unvalidatedValueProvider.GetValue(key, !performRequestValidation)
                                : bindingContext.ValueProvider.GetValue(key);

            if (key.Contains("[]"))
            {
                if (providerResult.RawValue.GetType() == typeof(string[]))
                {
                    var rawValues = providerResult.RawValue as string[];

                    if (rawValues.Count() == 2)
                    {
                        try
                        {
                            return bool.Parse(rawValues[0]) || bool.Parse(rawValues[1]);
                        } catch(Exception) { }
                    }
                    throw new Exception("Error in ParialModelBinder. Array type found that wasn't handled correctly.");
                }
                return providerResult.RawValue;
            }
            return providerResult.AttemptedValue;
        }
    }

    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> ForAllExpandoMembers<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt =>
            {
                opt.Condition(c => c.IsSourceValueNull ? false : c.DestinationType.Equals(typeof(string)) ? (c.SourceValue.ToString() != string.Empty || (c.DestinationValue != null && c.DestinationValue.ToString() != string.Empty)) : true);
                opt.ResolveUsing(res =>
                {
                    if (((IDictionary<string, object>)res.Context.SourceValue).ContainsKey(res.Context.MemberName))
                    {
                        return ((IDictionary<string, object>)res.Context.SourceValue)[res.Context.MemberName];
                    }
                    return null;
                });
            });
            return expression;
        }
    }
}
