using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation
{
    public class DataAnnotationsValidator
    {
        public bool TryValidate(object @object, out ICollection<ValidationResult> results)
        {
            ValidationContext context = new ValidationContext(@object, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                @object,
                context,
                results,
                validateAllProperties: true);
        }
    }
}
