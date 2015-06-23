using System;
using CurrencyExchange.Exceptions;
using CurrencyExchange.Logic;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Unit
{
    [TestFixture]
    public class PreviousDayCalculatorTests
    {
        private PreviousDayCalculator _previousDayCalculator;

        [SetUp]
        public void SetUp()
        {
            _previousDayCalculator = new PreviousDayCalculator();
        }

        [Test]
        public void GivenMondayWhenCalculatingPreviousDayThenShouldReturnFriday()
        {
            var previousDay = _previousDayCalculator.GetPreviousDay(new DateTime(2015, 6, 22));

            Assert.AreEqual(new DateTime(2015, 6, 19), previousDay);
        }

        [Test]
        public void GivenFirstDayOfMonthyWhenCalculatingPreviousDayThenShouldReturnLastDayOfPreviousMonth()
        {
            var previousDay = _previousDayCalculator.GetPreviousDay(new DateTime(2015, 5, 1));

            Assert.AreEqual(new DateTime(2015, 4, 30), previousDay);
        }

        [Test]
        public void GivenDateBeforeFirstInFileWhenCalculatingPreviousDayThenShouldThrowException()
        {
            Assert.Throws<IncorrectDateException>(() => _previousDayCalculator.GetPreviousDay(new DateTime(2000, 5, 1)));
        }

        [Test]
        public void GivenDateAfterTodayWhenCalculatingPreviousDayThenShouldThrowException()
        {
            Assert.Throws<IncorrectDateException>(() => _previousDayCalculator.GetPreviousDay(DateTime.Now.AddDays(2)));
        }
    }
}