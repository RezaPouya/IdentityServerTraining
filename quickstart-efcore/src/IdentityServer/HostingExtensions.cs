using IdentityServer.Data;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        //builder.Services.AddIdentityServer(options =>
        //    {
        //        // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
        //        options.EmitStaticAudienceClaim = true;
        //    })
        //    .AddInMemoryIdentityResources(IdentityResourceConfig.GetIdentityResources())
        //    .AddInMemoryApiScopes(ApiScopeConfig.GetApiScopes())
        //    .AddTestUsers(IdentityUserConfig.GetTestUsers())
        //    .AddInMemoryClients(ClientConfig.GetClients());

        builder.AddUdentityServerWithEfCore();

        //builder.AddGoogle();
        builder.AddGoogleWithCloudDemo();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        InitializeData.InitializeDatabase(app);

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}