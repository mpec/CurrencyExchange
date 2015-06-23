using System.Linq;
using CurrencyExchange.Logic;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Unit
{
    [TestFixture]
    public class CurrencyConverterTests
    {
        private CurrencyConverter _currencyConverter;

        [SetUp]
        public void SetUp()
        {
            _currencyConverter = new CurrencyConverter();
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldReturnCorrectNumberOfItems()
        {
            var result = _currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual(35, result.Count);
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveTheCorectDateSetUp()
        {
            var result = _currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.IsTrue(result.Keys.All(x => x.Date == MockData.GetDate()));
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveDifferencCurrencies()
        {
            var result = _currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual(35, result.Keys.Select(x => x.Currency).Distinct().Count());
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveCorrectCurrencyValue()
        {
            var result = _currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual((decimal)2.9915, result.First(x => x.Key.Currency == "USD").Value.Value);
        }
    }
}