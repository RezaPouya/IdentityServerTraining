using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.IdentityConfigs;

namespace IdentityServer.Data
{
    internal static class InitializeData
    {
        internal static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                AddConfigurationsInitailData(serviceScope);

                Initialize_ApplicationDbContext(serviceScope);
                InitializeDataRoles.AddRoles(serviceScope);
                InitializeDataUsers.AddUsers(serviceScope);
            }
        }

        private static void Initialize_ApplicationDbContext(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        private static void AddConfigurationsInitailData(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            AddClients(context);

            AddIdentityResources(context);

            AddApiScopes(context);
        }

        private static void AddClients(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in ClientConfig.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void AddIdentityResources(ConfigurationDbContext context)
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityResourceConfig.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        private static void AddApiScopes(ConfigurationDbContext context)
        {
            if (!context.ApiScopes.Any())
            {
                foreach (var resource in ApiScopeConfig.GetApiScopes())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}