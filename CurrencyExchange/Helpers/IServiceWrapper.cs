using System.Xml.Linq;

namespace CurrencyExchange.Helpers
{
    public interface IServiceWrapper
    {
        XDocument GetCurrencyData(string url);
        string GetUrl(string endOfFileName);
    }
}