using System;
using System.Linq;
using CurrencyExchange.Helpers;
using CurrencyExchange.Infrastructure;
using CurrencyExchange.Models;

namespace CurrencyExchange.Logic
{
    public class CurrencyProvider
    {
        private readonly IPreviousDayCalculator _previousDayCalculator;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IArchivedDataProvider _archivedDataProvider;

        public CurrencyProvider(IPreviousDayCalculator previousDayCalculator, ICurrencyConverter currencyConverter, IServiceWrapper serviceWrapper, IArchivedDataProvider archivedDataProvider)
        {
            _previousDayCalculator = previousDayCalculator;
            _currencyConverter = currencyConverter;
            _serviceWrapper = serviceWrapper;
            _archivedDataProvider = archivedDataProvider;
        }

        public CurrencyResult Get(CurrencyRequest data)
        {
            data.Date = data.Date.Date;
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
            var archivedValue = _archivedDataProvider.Get(date, currencySymbol);
            if (archivedValue != null)
            {
                return archivedValue;
            }

            var endOfFileName = date.ToString("yyMMdd");
            var url = _serviceWrapper.GetUrl(endOfFileName);
            var currencyDataXml = _serviceWrapper.GetCurrencyData(url);
            var currencyValues = _currencyConverter.Convert(date, currencyDataXml);
            _archivedDataProvider.Save(currencyValues);

            return currencyValues.First(x => x.Key.Currency == currencySymbol).Value;
        }

        private Trend CalculateTrend(decimal current, decimal previous)
        {
            if (current > previous)
            {
                return Trend.Growing;
            }

            if (current < previous)
            {
                return Trend.Decreasing;
            }

            return Trend.Same;
        }
    }
}