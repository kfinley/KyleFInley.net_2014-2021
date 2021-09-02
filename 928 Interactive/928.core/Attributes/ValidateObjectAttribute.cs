using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _928.Core.Attributes
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        private bool allowEmptyObject = false;

        public bool AllowEmptyObject
        {
            get { return allowEmptyObject; }
            set { allowEmptyObject = value; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);

            if (results.Count != 0)
            {
                var compositeResults = new CompositeValidationResult(String.Format("Validation for {0} failed!", validationContext.DisplayName));
                results.ForEach(compositeResults.AddResult);

                return compositeResults;
            }
            if (allowEmptyObject == false)
            {
                if (value.Equals(Activator.CreateInstance(context.ObjectType)))
                {
                    return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }

    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return results;
            }
        }

        public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public void AddResult(ValidationResult validationResult)
        {
            results.Add(validationResult);
        }
    }
}
