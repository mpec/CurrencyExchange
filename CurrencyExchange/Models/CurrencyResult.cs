using System;

namespace CurrencyExchange.Models
{
    public class CurrencyResult
    {
        public decimal Value { get; set; } 
        public Trend Trend { get; set; }
    }
}