using Microsoft.AspNetCore.Authentication;

namespace WebPortal.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = "Cookies";
                opts.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", opts =>
                {
                    opts.Authority = "https://localhost:6001";
                    opts.ClientId = "WebPortalClient";
                    opts.ClientSecret = "secret";
                    opts.ResponseType = "code";
                    opts.Scope.Clear();
                    opts.Scope.Add("openid");
                    opts.Scope.Add("profile");
                    opts.SaveTokens = true;

                    //  اضافه شدن سایر اطلاعات کاربر ( کلایم) از لینک مربوطه
                    // [idneity-url]/userinfo
                    opts.GetClaimsFromUserInfoEndpoint = true;

                    opts.Scope.Add("verification");
                    opts.ClaimActions.MapJsonKey("email_verified", "email_verified");

                    // quickstart 3 ( ASP.NET Core and API access ) -- refresh-token
                    opts.Scope.Add("OrderService");
                    opts.Scope.Add("offline_access");
                });
        }
    }
}