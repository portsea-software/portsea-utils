using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Portsea.Utils.Validation.Annotations
{
    public class MinEnumerableLengthAttribute : ValidationAttribute
    {
        private readonly int minLength;

        public MinEnumerableLengthAttribute(int minimumCollectionLength)
        {
            this.minLength = minimumCollectionLength;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var enumerable = (value as IEnumerable).Cast<object>();
                if (enumerable != null)
                {
                    return enumerable.Count() >= this.minLength;
                }
            }

            return false;
        }
    }
}
