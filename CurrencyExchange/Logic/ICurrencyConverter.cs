using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CurrencyExchange.Models;

namespace CurrencyExchange.Logic
{
    public interface ICurrencyConverter
    {
        IDictionary<CurrencyKey, CurrencyValue> Convert(DateTime date, XDocument document);
    }
}