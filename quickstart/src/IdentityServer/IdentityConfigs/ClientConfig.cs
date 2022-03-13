using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityConfigs;

public static class ClientConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new Client[]
        {
            new Client
            {
                ClientId = "OrderServiceClient",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "InvoiceService"  , "AccountService"}
            },

            new Client
            {
                ClientId = "InvoiceServiceClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "OrderService"  , "AccountService"}
            },

            new Client
            {
                ClientId = "AccountServiceClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "OrderService"  , "InvoiceService"}
            },
            // interactive ASP.NET Core Web App
            new Client {
                ClientId = "WebPortalClient",
                ClientSecrets = { new Secret ("secret".Sha256())},
                AllowedGrantTypes  = GrantTypes.Code ,
                // where to redirect to after login
                RedirectUris = { "https://localhost:8002/signin-oidc" },
                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:8002/signout-callback-oidc" },

                AllowedScopes = { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification"
                }
            }
        };
    }
}