using Microsoft.IdentityModel.Tokens;

namespace InvoiceService.Configurations
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:6001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }

        public static void AddIdentityAddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "InvoiceService");
                });
            });
        }
    }
}