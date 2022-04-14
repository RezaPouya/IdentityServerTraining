using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer.Clients
{
    public static class ReactClientApp
    {
        public static Client GetReactClientApp()
        {
            return new Client
            {
                ClientId = "react-app-client",
                ClientName = "React Client App ",
                AllowedGrantTypes = GrantTypes.Code,
               // RequirePkce = true,
                //AllowRememberConsent = true,
                AllowOfflineAccess = true,
                RedirectUris = new List<string>() {
                    "http://localhost:4200/authentication/callback"  ,
                    //"http://localhost:4200/signin-oidc" 
                },
                PostLogoutRedirectUris = new List<string>() { 
                    "http://localhost:4200/signout-callback-oidc" 
                },

                //RedirectUris = new List<string>() { "http://localhost:4200/authentication/callback" },
                //PostLogoutRedirectUris = new List<string>() { "http://localhost:4200/signout-callback-oidc" },
                RequireClientSecret = false,

                //ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },

                AllowedCorsOrigins = { "http://localhost:4200"  , "https://localhost:5005" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    //IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    //"movieAPI",
                    "roles",
                    "offline_access"
                },

                //AllowAccessTokensViaBrowser = true

            };
        }
    }
}