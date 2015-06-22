using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using CurrencyExchange.Models;

namespace CurrencyExchange.Logic
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public IDictionary<CurrencyKey, CurrencyValue> Convert(DateTime date, XDocument document)
        {
            var positions = document.Descendants("pozycja");

            return positions
                .Select(x => new KeyValuePair<CurrencyKey, CurrencyValue>(
                    new CurrencyKey
                    {
                        Date = date,
                        Currency = x.Descendants("kod_waluty").First().Value
                    },
                    new CurrencyValue
                    {
                        Value = decimal.Parse(x.Descendants("kurs_sredni").First().Value.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture)
                    }))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}