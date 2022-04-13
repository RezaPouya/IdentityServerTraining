using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

namespace OrderProcessing.Configurations
{
    public static class OcelotConfiguration
    {
        public static void AddOcelotConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("ocelot.json");

            builder.Services
                .AddOcelot(builder.Configuration)
                .AddCacheManager(opt => opt.WithDictionaryHandle());
        }
    }
}