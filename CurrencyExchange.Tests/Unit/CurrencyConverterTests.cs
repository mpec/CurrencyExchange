using System.Linq;
using CurrencyExchange.Logic;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Unit
{
    [TestFixture]
    public class CurrencyConverterTests
    {
        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldReturnCorrectNumberOfItems()
        {
            var currencyConverter = new CurrencyConverter();
            var result = currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual(35, result.Count);
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveTheCorectDateSetUp()
        {
            var currencyConverter = new CurrencyConverter();
            var result = currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.IsTrue(result.Keys.All(x => x.Date == MockData.GetDate()));
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveDifferencCurrencies()
        {
            var currencyConverter = new CurrencyConverter();
            var result = currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual(35, result.Keys.Select(x => x.Currency).Distinct().Count());
        }

        [Test]
        public void GivenSampleXmlWhenConvertingDataShouldHaveCorrectCurrencyValue()
        {
            var currencyConverter = new CurrencyConverter();
            var result = currencyConverter.Convert(MockData.GetDate(), MockData.GetSampleData());

            Assert.AreEqual((decimal)2.9915, result.First(x => x.Key.Currency == "USD").Value.Value);
        }
    }
}