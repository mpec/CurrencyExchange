using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CurrencyExchange.Helpers;
using CurrencyExchange.Logic;
using CurrencyExchange.Models;
using Moq;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Unit
{
    [TestFixture]
    public class CurrencyProviderTests
    {
        private const string Currency = "GBP";
        private const string UnusedCurrency = "EUR";
        private CurrencyProvider _currencyProvider;
        private DateTime _date;
        private XDocument _xDocument;
        private Dictionary<CurrencyKey, CurrencyValue> _currencyValues;
        private DateTime _previousDate;
        private Dictionary<CurrencyKey, CurrencyValue> _previousCurrencyValues;

        [SetUp]
        public void SetUp()
        {
            _date = new DateTime(2010, 11, 19);
            _previousDate = new DateTime(2010, 11, 18);
            _xDocument = XDocument.Parse("<xml></xml>");
            _currencyValues = new Dictionary<CurrencyKey, CurrencyValue>
            {
                {
                    new CurrencyKey
                    {
                        Currency = Currency,
                        Date = _date
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.14
                    }
                },
                {
                    new CurrencyKey
                    {
                        Currency = UnusedCurrency,
                        Date = _date
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.22
                    }
                }
            };

            _previousCurrencyValues = new Dictionary<CurrencyKey, CurrencyValue>
            {
                {
                    new CurrencyKey
                    {
                        Currency = Currency,
                        Date = _previousDate
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.14
                    }
                },
                {
                    new CurrencyKey
                    {
                        Currency = UnusedCurrency,
                        Date = _previousDate
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.40
                    }
                }
            };

            var currencyConverterMock = new Mock<ICurrencyConverter>();
            currencyConverterMock.Setup(x => x.Convert(_date, _xDocument)).Returns(_currencyValues);
            currencyConverterMock.Setup(x => x.Convert(_previousDate, _xDocument)).Returns(_previousCurrencyValues);

            var previousDayCalculatorMock = new Mock<IPreviousDayCalculator>();
            previousDayCalculatorMock.Setup(x => x.GetPreviousDay(_date)).Returns(_previousDate);
            var serviceWrapperMock = new Mock<IServiceWrapper>();
            serviceWrapperMock.Setup(x => x.GetUrl(It.IsAny<string>())).Returns("url");
            serviceWrapperMock.Setup(x => x.GetCurrencyData("url")).Returns(_xDocument);

            _currencyProvider = new CurrencyProvider(previousDayCalculatorMock.Object, currencyConverterMock.Object, serviceWrapperMock.Object);
        }

        [Test]
        public void GivenCurrencyRequestGettingDataThenReturnsExpectedResult()
        {
            var currencyRequest = new CurrencyRequest
            {
                Currency = Currency,
                Date = _date
            };

            var currencyResult = _currencyProvider.Get(currencyRequest);

            Assert.AreEqual(3.14, currencyResult.Value);
            Assert.AreEqual(Trend.Same, currencyResult.Trend);
        }
    }
}