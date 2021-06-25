using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation.Annotations
{
    public class IsTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value is bool)
            {
                return (bool)value;
            }

            return false;
        }
    }
}
