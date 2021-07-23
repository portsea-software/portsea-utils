namespace Portsea.Utils.Validation.Annotations
{
    public class GreaterThanAttribute : NumericComparisonAttribute
    {
        public GreaterThanAttribute(double pivotValue)
            : base(pivotValue)
        {
        }

        protected override bool Compare<T>(T value, T pivotValue)
        {
            return value.CompareTo(pivotValue) > 0;
        }
    }
}
