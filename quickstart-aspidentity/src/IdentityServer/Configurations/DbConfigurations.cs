using IdentityConfigs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServer.Configurations
{
    public static class DbConfigurations
    {
        public static void AddUdentityServerWithEfCore(this WebApplicationBuilder builder)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            string connectionString = builder.Configuration["ConnectionStrings:Default"];

            builder.Services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddTestUsers(IdentityUserConfig.GetTestUsers());
        }
    }
}