using CurrencyExchange.Models;

namespace CurrencyExchange.Logic
{
    public interface ICurrencyProvider
    {
        CurrencyResult Get(CurrencyRequest data);
    }
}