using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public string GetUrl(string endOfFileName)
        {
            var url = string.Format("{0}{1}", Const.WebServiceBaseUrl, Const.DirFile);
            WebClient webClient = new WebClient();
            var stream = webClient.OpenRead(url);
            if (stream == null)
            {
                throw new DocumentNotFoundException();                
            }

            var lines = new List<string>();

            using (var streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            //TODO: create a class for this we can test this logic
            var urlForDate = lines.FirstOrDefault(x => x.EndsWith(endOfFileName) && x.StartsWith(Const.FilePrefix));

            if (urlForDate == null)
            {
                throw new DocumentNotFoundException();
            }

            return string.Format("{0}{1}.xml", Const.WebServiceBaseUrl, urlForDate);
        }
    }
}