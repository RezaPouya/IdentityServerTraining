using Duende.Bff.Yarp;
using JavaScriptClient.Configurations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddBff().AddRemoteApis();

builder.Services.AddIdentityAuthentication();

//builder.Services.AddControllers();

//builder.Services.AddTransient<ILoginService, DefaultLoginService>();
//builder.Services.AddTransient<ILogoutService, DefaultLogoutService>();
//builder.Services.AddTransient<IUserService, DefaultUserService>();
//builder.Services.AddTransient<IBackchannelLogoutService, DefaultBackchannelLogoutService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseBff();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBffManagementEndpoints();

    //endpoints.MapControllers().AsBffApiEndpoint();

    endpoints.MapGet("/local/identity", LocalIdentityHandler)
    .AsBffApiEndpoint();

    endpoints
    .MapRemoteBffApiEndpoint("/remote", "https://localhost:7003")
    .RequireAccessToken(Duende.Bff.TokenType.User);
});

app.Run();


[Authorize]
static IResult LocalIdentityHandler(ClaimsPrincipal user)
{
    var name = user.FindFirst("name")?.Value ?? user.FindFirst("sub")?.Value;
    return Results.Json(new { message = "Local API Success!", user = name });
}