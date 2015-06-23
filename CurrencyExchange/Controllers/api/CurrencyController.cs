using System.Web.Http;
using CurrencyExchange.Helpers;
using CurrencyExchange.Logic;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers.api
{
    public class CurrencyController : ApiController
    {
        private readonly CurrencyProvider _currencyProvider;

        public CurrencyController()
        {
            _currencyProvider = new CurrencyProvider(new PreviousDayCalculator(), new CurrencyConverter(), new ServiceWrapper()); //TODO: IoC
        }

        public CurrencyResult Get([FromUri]CurrencyRequest data)
        {
            var currencyResult = _currencyProvider.Get(data);

            return currencyResult;
        }
    }
}
