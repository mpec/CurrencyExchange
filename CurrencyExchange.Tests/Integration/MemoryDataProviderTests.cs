using System;
using System.Collections.Generic;
using CurrencyExchange.Infrastructure;
using CurrencyExchange.Models;
using NUnit.Framework;

namespace CurrencyExchange.Tests.Integration
{
    [TestFixture]
    public class MemoryDataProviderTests
    {
        [Test]
        public void WhenGettingANewValueGivenItIsInCacheThenReturnThatValue()
        {
            var date = DateTime.Now;
            var currency = "USD";
            var currencyValues = new Dictionary<CurrencyKey, CurrencyValue>
            {
                {
                    new CurrencyKey
                    {
                        Currency = currency,
                        Date = date
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.14
                    }
                },
                {
                    new CurrencyKey
                    {
                        Currency = "x",
                        Date = date
                    },
                    new CurrencyValue
                    {
                        Value = (decimal) 3.22
                    }
                }
            };

            var memoryDataProvider = new MemoryDataProvider();
            var firstRead = memoryDataProvider.Get(date, currency);
            memoryDataProvider.Save(currencyValues);
            var secondRead = memoryDataProvider.Get(date, currency);

            Assert.IsNull(firstRead);
            Assert.AreEqual((decimal)3.14, secondRead.Value);
        }
    }
}