using CurrencyExchange.Exceptions;
using CurrencyExchange.Helpers;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Integration
{
    [TestFixture]
    public class ServiceWrapperTests
    {
        [Test]
        [Ignore]
        public void GivenCorrectUrlWhenWeTryToGetCurrencyDataThenWeShouldGetXDocument()
        {
            var serviceWrapper = new ServiceWrapper();
            
            var result = serviceWrapper.GetCurrencyData(string.Format("{0}{1}", Const.WebServiceBaseUrl, "a025z100205.xml"));
            
            Assert.AreEqual("tabela_kursow", result.Root.Name.ToString());
        }

        [Test]
        [Ignore]
        public void GivenIncorrectUrlWhenWeTryToGetCurrencyDataThenExceptonShouldBeThrown()
        {
            var serviceWrapper = new ServiceWrapper();

            Assert.Throws<DocumentNotFoundException>(() => serviceWrapper.GetCurrencyData(string.Format("{0}{1}", Const.WebServiceBaseUrl, "a025z100217.xml")));
        }

        [Test]
        [Ignore]
        public void GivenCorrectDateWhenWeTryToGetUrlThenWeShouldGetAValidUrl()
        {
            var serviceWrapper = new ServiceWrapper();

            var result = serviceWrapper.GetUrl("020102");

            Assert.AreEqual("http://www.nbp.pl/kursy/xml/a001z020102.xml", result);
        }

        [Test]
        [Ignore]
        public void GivenInCorrectDateWhenWeTryToGeUrlThenExceptonShouldBeThrown()
        {
            var serviceWrapper = new ServiceWrapper();

            Assert.Throws<DocumentNotFoundException>(() => serviceWrapper.GetUrl("020105"));
        }
    }
}