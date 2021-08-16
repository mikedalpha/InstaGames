using System;
using GroupProject.WebApp.Helpers;
using NUnit.Framework;

namespace GroupProject.Tests
{
    [TestFixture]
    public class UtilitiesTests
    {
        [Test]
        public static void SummarizeTextTest()
        {
            var text = "Have a nice Day Coding";
            var maxLength = 15;
            var result = Utilities.SummarizeText(text, maxLength);

            Assert.That(result, Is.EqualTo("Have a nice Day..."));
        }

        [Test]
        public static void CalculateAgeTests()
        {
            var dateOfBirth = new DateTime(1995,10,28);
            var age = Utilities.CalculateAge(dateOfBirth);

            Assert.That(age, Is.EqualTo(25));
        }
    }
}
