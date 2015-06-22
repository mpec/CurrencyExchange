using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using CurrencyExchange.Exceptions;

namespace CurrencyExchange.Helpers
{
    public class ServiceWrapper : IServiceWrapper
    {
        public XDocument GetCurrencyData(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                var stream = webClient.OpenRead(url);
                var document = XDocument.Load(stream);
                return document;
            }
            catch (Exception ex)
            {
                //log the original exception
                // the NBP api doesn't give us much info we assume a file doesn't exist if there was an exception
                throw new DocumentNotFoundException();
            }
        } 
    }
}