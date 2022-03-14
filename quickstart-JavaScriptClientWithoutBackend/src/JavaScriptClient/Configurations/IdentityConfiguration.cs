using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace JavaScriptClient.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityAuthentication(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = "Cookies";
                opts.DefaultChallengeScheme = "oidc";
                opts.DefaultSignOutScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", opts =>
                {
                    opts.Authority = "https://localhost:6001";
                    opts.ClientId = "BackendForFrontendClient";
                    opts.ClientSecret = "secret";
                    opts.ResponseType = "code";

                    //opts.Scope.Clear();
                    opts.SaveTokens = true;
                    opts.GetClaimsFromUserInfoEndpoint = true;

                    opts.Scope.Add("openid");
                    opts.Scope.Add("profile");
                    opts.Scope.Add("OrderService");
                    opts.Scope.Add("verification");
                    opts.ClaimActions.MapJsonKey("email_verified", "email_verified");
                    opts.Scope.Add("offline_access");
                });
        }
    }
}