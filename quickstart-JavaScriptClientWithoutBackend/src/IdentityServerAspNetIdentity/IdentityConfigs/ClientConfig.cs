using Duende.IdentityServer.Models;

namespace IdentityServerAspNetIdentity.IdentityConfigs;

public static class ClientConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new Client[]
        {
            new Client
            {
                ClientId = "OrderServiceClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
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

                //Enable support for refresh tokens by setting the AllowOfflineAccess flag
                AllowOfflineAccess = true,

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "OrderService"
                }
            },
            new Client
            {
                ClientId = "BackendForFrontendClient",
                ClientSecrets = { new Secret ("secret".Sha256())},
                AllowedGrantTypes  = GrantTypes.Code ,
                RedirectUris = { "https://localhost:9003/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:9003/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes = new List<string> {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "OrderService",
                    "InvoiceService"
                }
            },
            new Client
            {
                ClientId = "Javascript_App",
                ClientName = "JavaScript Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RedirectUris =           { "https://localhost:10002/callback.html" },
                PostLogoutRedirectUris = { "https://localhost:10002/index.html" },
                AllowedCorsOrigins =     { "https://localhost:10002" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "OrderService",
                    "InvoiceService"
                }
            }
        };
    }
}