using System;

namespace CurrencyExchange.Models
{
    public class CurrencyRequest
    {
        public string CurrencySymbol { get; set; } 
        public DateTime Date { get; set; } 
    }
}