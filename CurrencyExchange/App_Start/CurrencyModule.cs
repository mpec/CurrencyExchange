using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using CurrencyExchange.Helpers;
using CurrencyExchange.Infrastructure;
using CurrencyExchange.Logic;
using Module = Autofac.Module;

namespace CurrencyExchange
{
    public class CurrencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CurrencyProvider>().As<ICurrencyProvider>();
            builder.RegisterType<ServiceWrapper>().As<IServiceWrapper>();
            builder.RegisterType<PreviousDayCalculator>().As<IPreviousDayCalculator>();
            builder.RegisterType<CurrencyConverter>().As<ICurrencyConverter>();
            builder.RegisterType<MemoryDataProvider>().As<IArchivedDataProvider>();
        }
    }
}