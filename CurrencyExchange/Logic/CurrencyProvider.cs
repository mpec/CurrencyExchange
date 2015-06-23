using System;
using System.Linq;
using CurrencyExchange.Helpers;
using CurrencyExchange.Models;

namespace CurrencyExchange.Logic
{
    public class CurrencyProvider
    {
        private readonly IPreviousDayCalculator _previousDayCalculator;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IServiceWrapper _serviceWrapper;

        public CurrencyProvider(IPreviousDayCalculator previousDayCalculator, ICurrencyConverter currencyConverter, IServiceWrapper serviceWrapper)
        {
            _previousDayCalculator = previousDayCalculator;
            _currencyConverter = currencyConverter;
            _serviceWrapper = serviceWrapper;
        }

        public CurrencyResult Get(CurrencyRequest data)
        {
            var previousDay = _previousDayCalculator.GetPreviousDay(data.Date);
            var currencyDataForDate = GetCurrencyData(data.Date, data.Currency);
            var currencyDataForPreviousDate = GetCurrencyData(previousDay, data.Currency);

            return new CurrencyResult
            {
                Value = currencyDataForDate.Value,
                Trend = CalculateTrend(currencyDataForDate.Value, currencyDataForPreviousDate.Value)
            };
        }

        private CurrencyValue GetCurrencyData(DateTime date, string currencySymbol)
        {
            var endOfFileName = date.ToString("yyMMdd");
            var url = _serviceWrapper.GetUrl(endOfFileName);
            var currencyDataXml = _serviceWrapper.GetCurrencyData(url);
            var currencyValues = _currencyConverter.Convert(date, currencyDataXml);

            return currencyValues.First(x => x.Key.Currency == currencySymbol).Value;
        }

        private Trend CalculateTrend(decimal current, decimal previous)
        {
            if (current > previous)
            {
                return Trend.Decreasing;
            }

            if (current < previous)
            {
                return Trend.Growing;
            }

            return Trend.Same;
        }
    }
}