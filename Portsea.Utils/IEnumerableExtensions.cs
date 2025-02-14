using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Portsea.Utils
{
    public static class IEnumerableExtensions
    {
        public static bool IsAny(this IEnumerable source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source != null)
            {
                return !source.IsAny();
            }

            return true;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source != null && source.Any())
            {
                return false;
            }

            return true;
        }
    }
}
