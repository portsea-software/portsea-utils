﻿using NUnit.Framework;
using Portsea.Utils.Validation.Annotations;

namespace Portsea.Utils.Tests.Validation.Attributes
{
    public class EmailAttributeTests
    {
        [TestCase(false, "example@example.com", true)]
        [TestCase(false, null, true)]
        [TestCase(false, "", false)]
        [TestCase(false, "  ", false)]
        [TestCase(true, "example@example.com", true)]
        [TestCase(true, null, true)]
        [TestCase(true, "", true)]
        [TestCase(true, "  ", true)]
        public void Check_With_Allow_Empty_Strings_Parameter(bool allowEmptyStrings, string emailAddress, bool isValid)
        {
            // Assert
            EmailAddressAttribute attribute = new EmailAddressAttribute();
            attribute.AllowEmptyStrings = allowEmptyStrings;

            Assert.AreEqual(isValid, attribute.IsValid(emailAddress));
        }
    }
}
