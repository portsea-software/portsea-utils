namespace Portsea.Utils.Validation.Annotations
{
    public class LessThanAttribute : NumericComparisonAttribute
    {
        public LessThanAttribute(double pivotValue)
            : base(pivotValue)
        {
        }

        protected override bool Compare<T>(T value, T pivotValue)
        {
            return value.CompareTo(pivotValue) < 0;
        }
    }
}
