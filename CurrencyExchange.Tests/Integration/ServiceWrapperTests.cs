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
        public void GivenCorrectUrlWhenWeTryToGetCurrencyDataThenWeShouldGetAXDocument()
        {
            var serviceWrapper = new ServiceWrapper();
            
            var result = serviceWrapper.GetCurrencyData(string.Format("{0}{1}", Const.WebServiceBaseUrl, "a025z100205.xml"));
            
            Assert.AreEqual("tabela_kursow", result.Root.Name.ToString());
        }

        [Test]
        [Ignore]
        public void GivenInCorrectUrlWhenWeTryToGetCurrencyDataExceptonShouldBeThrown()
        {
            var serviceWrapper = new ServiceWrapper();

            Assert.Throws<DocumentNotFoundException>(() => serviceWrapper.GetCurrencyData(string.Format("{0}{1}", Const.WebServiceBaseUrl, "a025z100217.xml")));
        }
    }
}