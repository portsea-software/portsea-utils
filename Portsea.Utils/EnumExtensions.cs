using System;
using System.Collections.Generic;

namespace Portsea.Utils
{
    public static class EnumExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum type)
        {
            foreach (Enum value in Enum.GetValues(type.GetType()))
            {
                if (type.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static IEnumerable<string> GetNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
