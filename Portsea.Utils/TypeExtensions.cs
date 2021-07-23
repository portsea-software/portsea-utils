using System;
using System.Collections.Generic;

namespace Portsea.Utils
{
    public static class TypeExtensions
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(sbyte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(decimal),
            typeof(double),
            typeof(float),
        };

        public static bool IsNumericType<T>()
        {
            return typeof(T).IsNumericType();
        }

        public static bool IsNumericType(this object value)
        {
            return value.GetType().IsNumericType();
        }

        public static bool IsNumericType(this Type type)
        {
            return NumericTypes.Contains(type);
        }
    }
}
