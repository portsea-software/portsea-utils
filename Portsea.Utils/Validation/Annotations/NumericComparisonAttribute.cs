using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation.Annotations
{
    public abstract class NumericComparisonAttribute : ValidationAttribute
    {
        public NumericComparisonAttribute(double pivotValue)
        {
            this.PivotValue = pivotValue;
        }

        protected double PivotValue { get; private set; }

        public override bool IsValid(object value)
        {
            if (value != null && value.IsNumericType())
            {
                Type valueType = value.GetType();
                if (valueType == typeof(byte))
                {
                    return this.Compare((byte)value,  Convert.ToByte(this.PivotValue));
                }
                else if (valueType == typeof(sbyte))
                {
                    return this.Compare((sbyte)value, Convert.ToSByte(this.PivotValue));
                }
                else if (valueType == typeof(ushort))
                {
                    return this.Compare((ushort)value, Convert.ToUInt16(this.PivotValue));
                }
                else if (valueType == typeof(uint))
                {
                    return this.Compare((uint)value, Convert.ToUInt32(this.PivotValue));
                }
                else if (valueType == typeof(ulong))
                {
                    return this.Compare((ulong)value, Convert.ToUInt64(this.PivotValue));
                }
                else if (valueType == typeof(short))
                {
                    return this.Compare((short)value, Convert.ToInt16(this.PivotValue));
                }
                else if (valueType == typeof(int))
                {
                    return this.Compare((int)value, Convert.ToInt32(this.PivotValue));
                }
                else if (valueType == typeof(long))
                {
                    return this.Compare((long)value, Convert.ToInt64(this.PivotValue));
                }
                else if (valueType == typeof(decimal))
                {
                    return this.Compare((decimal)value, Convert.ToDecimal(this.PivotValue));
                }
                else if (valueType == typeof(double))
                {
                    return this.Compare(Convert.ToDouble(value), this.PivotValue);
                }
                else if (valueType == typeof(float))
                {
                    return this.Compare((float)value, Convert.ToSingle(this.PivotValue));
                }
            }

            return false;
        }

        protected abstract bool Compare<T>(T value, T pivotValue)
            where T : IComparable;
    }
}
