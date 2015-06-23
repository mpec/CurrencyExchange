using System;
using System.Collections.Generic;
using CurrencyExchange.Models;

namespace CurrencyExchange.Infrastructure
{
    public interface IArchivedDataProvider
    {
        CurrencyValue Get(DateTime date, string currency);
        void Save(IDictionary<CurrencyKey, CurrencyValue> values);
    }
}