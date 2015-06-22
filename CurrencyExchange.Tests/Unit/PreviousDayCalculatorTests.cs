using System;
using CurrencyExchange.Exceptions;
using CurrencyExchange.Logic;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Unit
{
    [TestFixture]
    public class PreviousDayCalculatorTests
    {
        [Test]
        public void GivenMondayWhenCalculatingPreviousDayThenShouldReturnFriday()
        {
            var previousDayCalculator = new PreviousDayCalculator();

            var previousDay = previousDayCalculator.GetPreviousDay(new DateTime(2015, 6, 22));

            Assert.AreEqual(new DateTime(2015, 6, 19), previousDay);
        }

        [Test]
        public void GivenFirstDayOfMonthyWhenCalculatingPreviousDayThenShouldReturnLastDayOfPreviousMonth()
        {
            var previousDayCalculator = new PreviousDayCalculator();

            var previousDay = previousDayCalculator.GetPreviousDay(new DateTime(2015, 5, 1));

            Assert.AreEqual(new DateTime(2015, 4, 30), previousDay);
        }

        [Test]
        public void GivenDateBeforeFirstInFileWhenCalculatingPreviousDayThenShouldThrowException()
        {
            var previousDayCalculator = new PreviousDayCalculator();

            Assert.Throws<IncorrectDateException>(() => previousDayCalculator.GetPreviousDay(new DateTime(2000, 5, 1)));
        }

        [Test]
        public void GivenDateAfterTodayWhenCalculatingPreviousDayThenShouldThrowException()
        {
            var previousDayCalculator = new PreviousDayCalculator();

            Assert.Throws<IncorrectDateException>(() => previousDayCalculator.GetPreviousDay(DateTime.Now.AddDays(2)));
        }
    }
}