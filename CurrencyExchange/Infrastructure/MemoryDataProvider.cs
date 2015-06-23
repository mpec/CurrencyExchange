using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CurrencyExchange.Models;
using WebGrease.Css.Extensions;

namespace CurrencyExchange.Infrastructure
{
    public class MemoryDataProvider : IArchivedDataProvider
    {
        private static readonly ConcurrentDictionary<CurrencyKey, CurrencyValue> Cache;
        
        static MemoryDataProvider()
        {
            Cache = new ConcurrentDictionary<CurrencyKey, CurrencyValue>();    
        }

        public CurrencyValue Get(DateTime date, string currency)
        {
            var currencyKey = new CurrencyKey
            {
                Currency = currency,
                Date = date
            };

            if (Cache.ContainsKey(currencyKey))
            {
                return Cache[currencyKey];
            }

            return null;
        }

        public void Save(IDictionary<CurrencyKey, CurrencyValue> values)
        {
            values.ForEach(x => Cache.GetOrAdd(x.Key, x.Value));
        }
    }
}