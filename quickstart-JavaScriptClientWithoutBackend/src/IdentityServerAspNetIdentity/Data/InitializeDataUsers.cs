using IdentityServerAspNetIdentity.Constants;

namespace IdentityServerAspNetIdentity.Data
{
    internal static class InitializeDataUsers
    {
        internal static void AddUsers(IServiceScope serviceScope)
        {
            var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            AddUser_Admin(userMgr);
            AddUser_Alice(userMgr);
            AddUser_Bob(userMgr);
        }

        private static void AddUser_Admin(UserManager<ApplicationUser> userMgr)
        {
            var admin = userMgr.FindByNameAsync(UserConstants.Admin).Result;
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@gajmarket.com",
                    EmailConfirmed = true
                };

                var result = userMgr.CreateAsync(admin, "@Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                var addRoleResult = userMgr.AddToRoleAsync(admin, RoleConstants.Admin).Result;

                if (addRoleResult.Succeeded is false)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Admin Admin"),
                            new Claim(JwtClaimTypes.GivenName, "Admin"),
                            new Claim(JwtClaimTypes.FamilyName, "Admin"),
                            new Claim(JwtClaimTypes.WebSite, "https://gajmarket.com"),
                            new Claim("location", "Gajmrarketplace")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }
        }

        private static void AddUser_Bob(UserManager<ApplicationUser> userMgr)
        {
            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };

                var result = userMgr.CreateAsync(bob, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                var addRoleResult = userMgr.AddToRoleAsync(bob, RoleConstants.Operator).Result;

                if (addRoleResult.Succeeded is false)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("bob created");
            }
            else
            {
                Log.Debug("bob already exists");
            }
        }

        private static void AddUser_Alice(UserManager<ApplicationUser> userMgr)
        {
            var alice = userMgr.FindByNameAsync("alice").Result;

            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };

                var result = userMgr.CreateAsync(alice, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                var addRoleResult = userMgr.AddToRoleAsync(alice, RoleConstants.Customer).Result;

                if (addRoleResult.Succeeded is false)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }
        }
    }
}