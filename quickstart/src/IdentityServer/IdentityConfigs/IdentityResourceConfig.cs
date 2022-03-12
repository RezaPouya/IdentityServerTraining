using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class IdentityResourceConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };
    }
}