using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Portsea.Utils.Date;

namespace Portsea.Utils.Tests.Date
{
    public class DateTimeExtensionTests
    {
        [TestCase("2000-06-30", "2025-01-01", 24)]
        [TestCase("2000-06-30", "2025-06-30", 25)]
        [TestCase("2000-06-30", "2025-12-31", 25)]
        [TestCase("2000-06-30", "2026-06-29", 25)]
        [TestCase("2000-06-30", "2026-06-30", 26)]
        [TestCase("2000-02-29", "2024-02-28", 23)]
        [TestCase("2000-02-29", "2024-02-29", 24)]
        [TestCase("2000-02-29", "2025-02-28", 24)]
        [TestCase("2000-02-29", "2025-03-01", 25)]
        public void Get_Age_InYearsYears(string dateOfBirth, string currentDate, int expectedAge)
        {
            // Arrange
            DateTime dob = DateTime.Parse(dateOfBirth);
            DateTime on = DateTime.Parse(currentDate);

            // Act
            int ageInYears = dob.AgeInYears(on);

            // Assert
            Assert.That(ageInYears, Is.EqualTo(expectedAge));
        }
    }
}
