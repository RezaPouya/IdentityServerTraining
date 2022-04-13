using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderProcessing.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static void AddAuthenticationConfiguration(this WebApplicationBuilder builder)
        {
            string authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
            string audience = builder.Configuration["Auth0:Audience"];

            var secret = "fsfasdf";
            var key = Encoding.UTF8.GetBytes(secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = true,
                };
                options.Authority = authority;
                options.Audience = audience;
            });
        }
    }
}