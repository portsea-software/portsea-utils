using System.ComponentModel.DataAnnotations;

using Portsea.Utils.Validation.Annotations;
using NUnit.Framework;

namespace Portsea.Utils.Tests.Validation.Attributes
{
    public class GreaterThanTests
    {
        [TestCase((double)0.00, (byte)1)]
        [TestCase((double)0.00, (sbyte)1)]
        [TestCase((double)0.00, (ushort)1)]
        [TestCase((double)0.00, (uint)1)]
        [TestCase((double)0.00, (ulong)1)]
        [TestCase((double)0.00, (short)1)]
        [TestCase((double)0.00, (int)1)]
        [TestCase((double)0.00, (long)1)]
        [TestCase((double)0.00, (double)1.00)]
        [TestCase((double)0.00, (float)1.00)]
        public void Check_Greater_Values_Return_True(double pivotValue, object value)
        {
            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.True);
        }

        [Test]
        public void Check_Greater_Values_Return_True_Decimal()
        {
            // Arrange
            double pivotValue = 0.00;
            decimal value = 1.00M;

            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.True);
        }

        [TestCase((double)0.00, (byte)0)]
        [TestCase((double)0.00, (sbyte)0)]
        [TestCase((double)0.00, (ushort)0)]
        [TestCase((double)0.00, (uint)0)]
        [TestCase((double)0.00, (ulong)0)]
        [TestCase((double)0.00, (short)0)]
        [TestCase((double)0.00, (int)0)]
        [TestCase((double)0.00, (long)0)]
        [TestCase((double)0.00, (double)0.00)]
        [TestCase((double)0.00, (float)0.00)]
        public void Check_Equal_Values_Return_False(double pivotValue, object value)
        {
            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.False);
        }

        [Test]
        public void Check_Equal_Values_Return_False_Decimal()
        {
            // Arrange
            double pivotValue = 0.00;
            decimal value = 0.00M;

            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.False);
        }

        [TestCase((double)1.00, (byte)0)]
        [TestCase((double)1.00, (sbyte)0)]
        [TestCase((double)1.00, (ushort)0)]
        [TestCase((double)1.00, (uint)0)]
        [TestCase((double)1.00, (ulong)0)]
        [TestCase((double)1.00, (short)0)]
        [TestCase((double)1.00, (int)0)]
        [TestCase((double)1.00, (long)0)]
        [TestCase((double)1.00, (double)0.00)]
        [TestCase((double)1.00, (float)0.00)]
        public void Check_Lesser_Values_Return_False(double pivotValue, object value)
        {
            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.False);
        }

        [Test]
        public void Check_Lesser_Values_Return_False_Decimal()
        {
            // Arrange
            double pivotValue = 1.00;
            decimal value = 0.00M;

            // Assert
            Assert.That(this.GetAttribute(pivotValue).IsValid(value), Is.False);
        }

        private ValidationAttribute GetAttribute(double pivotValue)
        {
            return new GreaterThanAttribute(pivotValue);
        }
    }
}
