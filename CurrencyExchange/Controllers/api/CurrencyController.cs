using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers.api
{
    public class CurrencyController : ApiController
    {
        public CurrencyResult Get(object data)
        {
            return new CurrencyResult
            {
                Value = (decimal)3.14,
                Trend = Trend.Growing
            };
        }
    }
}
