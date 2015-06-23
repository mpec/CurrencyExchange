using System;
using System.Net;
using System.Web.Http;
using CurrencyExchange.Exceptions;
using CurrencyExchange.Helpers;
using CurrencyExchange.Infrastructure;
using CurrencyExchange.Logic;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers.api
{
    public class CurrencyController : ApiController
    {
        private readonly ICurrencyProvider _currencyProvider;

        public CurrencyController(ICurrencyProvider currencyProvider)
        {
            _currencyProvider = currencyProvider;
        }

        public CurrencyResult Get([FromUri]CurrencyRequest data)
        {
            try
            {
                var currencyResult = _currencyProvider.Get(data);

                return currencyResult;
            }
            catch (IncorrectDateException ex)
            {
                //log exception
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            catch (DocumentNotFoundException ex)
            {
                //log exception
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
