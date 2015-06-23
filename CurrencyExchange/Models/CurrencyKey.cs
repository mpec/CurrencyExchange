using System;

namespace CurrencyExchange.Models
{
    public class CurrencyKey
    {
        public string Currency { get; set; }
        public DateTime Date { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CurrencyKey))
            {
                return false;
            }

            var key = (CurrencyKey) obj;

            return Currency == key.Currency && Date == key.Date;
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode() ^ Currency.GetHashCode();
        }
    }
}