using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using CurrencyExchange.Helpers;
using CurrencyExchange.Models;

namespace CurrencyExchange.Controllers.api
{
    public class CurrencyController : ApiController
    {
        public CurrencyResult Get([FromUri]CurrencyRequest data)
        {
            var serviceWrapper = new ServiceWrapper();
            //TODO: missing: extract logic to other class, cache
            var urlForDate = serviceWrapper.GetUrl(data.Date.ToString("yyMMdd"));
            var dataforDate = serviceWrapper.GetCurrencyData(urlForDate);


            return new CurrencyResult
            {
                Value = (decimal)3.14,
                Trend = Trend.Growing
            };
        }
    }
}
