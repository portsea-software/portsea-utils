using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation
{
    public static class AnnotationsValidator
    {
        public static void Validate(this object @object)
        {
            if (@object == null)
            {
                throw new NullReferenceException("Cannot validate a null reference");
            }

            ValidationContext context = new ValidationContext(@object, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(
                @object,
                context,
                results,
                validateAllProperties: true);

            if (results.Count > 0)
            {
                throw new ValidationException(results);
            }
        }
    }
}
