using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using CurrencyExchange.Context;
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
            var useDbForPersistance = bool.Parse(ConfigurationManager.AppSettings["UseDatabaseForPersistance"]); // would be better to use Castle DictionaryAdapter
            base.Load(builder);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CurrencyProvider>().As<ICurrencyProvider>();
            builder.RegisterType<ServiceWrapper>().As<IServiceWrapper>();
            builder.RegisterType<PreviousDayCalculator>().As<IPreviousDayCalculator>();
            builder.RegisterType<CurrencyConverter>().As<ICurrencyConverter>();
            
            if (useDbForPersistance)
            {
                builder.Register(x => new CurrencyEntities()).InstancePerApiRequest();
                builder.RegisterType<DatabaseDataProvider>().As<IArchivedDataProvider>();
            }
            else
            {
                builder.RegisterType<MemoryDataProvider>().As<IArchivedDataProvider>();
            }
        }
    }
}