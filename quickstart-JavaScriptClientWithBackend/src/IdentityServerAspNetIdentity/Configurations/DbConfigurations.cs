using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServerAspNetIdentity.Configurations;

public static class DbConfigurations
{
    public static void AddIdentityDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddIdentityServerStore(this WebApplicationBuilder builder)
    {
        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        // ConfigurationDbContext: used for configuration data such as clients, resources, and scopes
        string configurationStore = builder.Configuration.GetConnectionString("ConfigurationStore");

        // PersistedGrantDbContext: used for dynamic operational data such as authorization codes and refresh tokens
        string operationalStore = builder.Configuration.GetConnectionString("OperationalStore");

        builder.Services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;

            // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
            options.EmitStaticAudienceClaim = true;
        })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(configurationStore,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(operationalStore,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>();
    }

    //builder.Services
    //    .AddIdentityServer(options =>
    //    {
    //        options.Events.RaiseErrorEvents = true;
    //        options.Events.RaiseInformationEvents = true;
    //        options.Events.RaiseFailureEvents = true;
    //        options.Events.RaiseSuccessEvents = true;

    //        // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
    //        options.EmitStaticAudienceClaim = true;
    //    })
    //    .AddInMemoryIdentityResources(Config.IdentityResources)
    //    .AddInMemoryApiScopes(Config.ApiScopes)
    //    .AddInMemoryClients(Config.Clients)
    //    .AddAspNetIdentity<ApplicationUser>();

}