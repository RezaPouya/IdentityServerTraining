using Duende.IdentityServer;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Configurations
{
    public static class ExternalAuthenticationConfigurations
    {
        /// <summary>
        /// When authenticating with Google, there are again two authentication schemes.
        /// AddGoogle adds the Google scheme, which handles the protocol flow back and forth with Google.
        /// After successful login, the application needs to sign in to an additional scheme that can authenticate future requests
        /// without needing a round-trip to Google - typically by issuing a local cookie.
        /// The SignInScheme tells the Google handler to use the scheme named IdentityServerConstants.ExternalCookieAuthenticationScheme,
        /// which is a cookie authentication handler automatically created by IdentityServer that is intended for external logins.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddGoogle(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
           .AddGoogle("Google", options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.ClientId = configuration["Authentication:Google:ClientId"];
               options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
           });
        }

        public static void AddGoogleWithCloudDemo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
           .AddGoogle("Google", options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.ClientId = configuration["Authentication:Google:ClientId"];
               options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
           })
           .AddOpenIdConnect("oidc", "Demo Identity Server", opts =>
         {
             opts.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
             opts.SignOutScheme = IdentityServerConstants.SignoutScheme;
             opts.SaveTokens = true;

             opts.Authority = "https://demo.duendesoftware.com";
             opts.ClientId = "interactive.confidential";
             opts.ClientSecret = "secret";
             opts.ResponseType = "code";
             opts.TokenValidationParameters = new TokenValidationParameters
             {
                 NameClaimType = "name",
                 RoleClaimType = "role"
             };
         });
        }
    }
}