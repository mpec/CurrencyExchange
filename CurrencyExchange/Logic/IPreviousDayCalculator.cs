using System;

namespace CurrencyExchange.Logic
{
    public interface IPreviousDayCalculator
    {
        DateTime GetPreviousDay(DateTime date);
    }
}