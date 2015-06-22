using System;
using CurrencyExchange.Exceptions;

namespace CurrencyExchange.Logic
{
    public class PreviousDayCalculator : IPreviousDayCalculator
    {
        public DateTime GetPreviousDay(DateTime date)
        {
            DateTime result = date;
            if (date.Date <= new DateTime(2002, 01, 02))
            {
                throw new IncorrectDateException();
            }

            if (date.Date > DateTime.Today)
            {
                throw new IncorrectDateException();
            }

            do
            {
                result = result.AddDays(-1);
            } while (result.DayOfWeek == DayOfWeek.Saturday || result.DayOfWeek == DayOfWeek.Sunday);

            return result;
        }
    }
}