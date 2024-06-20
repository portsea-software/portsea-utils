using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Portsea.Utils.Date;

namespace Portsea.Utils.Tests.Date
{
    public class TimeSpanExtensionTests
    {
        [TestCase("2020-02-29", "2028-02-29", 8)]
        [TestCase("2020-02-29", "2028-02-28", 7)]
        [TestCase("1959-01-01", "2022-01-01", 63)]
        [TestCase("1960-01-01", "2022-12-31", 62)]
        [TestCase("1980-01-01", "2022-06-01", 42)]
        [TestCase("1981-01-01", "2022-06-01", 41)]
        [TestCase("1983-01-01", "2022-06-01", 39)]
        [TestCase("1985-01-01", "2022-06-01", 37)]
        [TestCase("1987-01-01", "2022-06-01", 35)]
        [TestCase("2014-01-01", "2022-12-31", 8)]
        [TestCase("2016-01-01", "2022-06-01", 6)]
        [TestCase("2018-01-01", "2022-06-01", 4)]
        public void Get_Years(string firstDate, string secondDate, int expectedYears)
        {
            // Arrange
            DateTime first = DateTime.Parse(firstDate);
            DateTime second = DateTime.Parse(secondDate);

            // Act
            int years = (second - first).Years();

            // Assert
            Assert.That(years, Is.EqualTo(expectedYears));
        }
    }
}
