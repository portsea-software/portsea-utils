using Portsea.Utils.Validation;
using NUnit.Framework;

namespace Portsea.Utils.Tests.Validation
{
    public class EmailValidatorTests
    {
        [TestCase("example@example.com", true)]
        [TestCase("a@a.d", true)]
        [TestCase("false@true", false)]
        [TestCase("notanemailaddress", false)]
        public void Is_Valid_Email_Address(string emailAddress, bool expectedResult)
        {
            // Act
            bool result = EmailValidator.IsValid(emailAddress);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

    }
}
