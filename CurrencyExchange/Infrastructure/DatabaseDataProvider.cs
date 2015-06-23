using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyExchange.Context;
using CurrencyExchange.Models;
using CurrencyValue = CurrencyExchange.Models.CurrencyValue;

namespace CurrencyExchange.Infrastructure
{
    class DatabaseDataProvider : IArchivedDataProvider
    {
        private readonly CurrencyEntities _currencyEntities;

        public DatabaseDataProvider(CurrencyEntities currencyEntities)
        {
            _currencyEntities = currencyEntities;
        }

        public CurrencyValue Get(DateTime date, string currency)
        {
            var value = _currencyEntities.CurrencyValues.FirstOrDefault(x => x.Date == date && x.Currency == currency);

            return value != null ? new CurrencyValue{Value = value.Value} : null;
        }

        public void Save(IDictionary<CurrencyKey, CurrencyValue> values)
        {
            var currencyValues = values.Select(x => new Context.CurrencyValue
            {
                Id = Guid.NewGuid(),
                Value = x.Value.Value,
                Currency = x.Key.Currency,
                Date = x.Key.Date
            });

            _currencyEntities.CurrencyValues.AddRange(currencyValues);
            _currencyEntities.SaveChanges(); // could add http request as a unit of work and save at the end of request
        }
    }
}