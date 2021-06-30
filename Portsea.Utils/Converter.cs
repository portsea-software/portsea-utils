using System.ComponentModel;

namespace Portsea.Utils
{
    public static class Converter
    {
        public static T Convert<T>(string value)
            where T : struct
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            return (T)typeConverter.ConvertFrom(value);
        }
    }
}
