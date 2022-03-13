using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityConfigs;

public static class IdentityResourceConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "verification",
                UserClaims = new string[]
                {
                    JwtClaimTypes.Email , 
                    JwtClaimTypes.EmailVerified,
                }
            }
        };
    }
}


// IdentityServer uses the IProfileService to retrieve claims for tokens and the userinfo endpoint.
// You can provide your own implementation of IProfileService to customize this process with custom logic,
// data access, etc. Since you are using AddTestUsers, the TestUserProfileService is used automatically.
// It will automatically include requested claims from the test users added in TestUsers.cs.

