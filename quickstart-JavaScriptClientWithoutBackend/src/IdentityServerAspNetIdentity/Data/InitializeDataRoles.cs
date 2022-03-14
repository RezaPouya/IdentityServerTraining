using IdentityServerAspNetIdentity.Constants;

namespace IdentityServer.Data
{
    internal static class InitializeDataRoles
    {
        internal static void AddRoles(IServiceScope serviceScope)
        {
            var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Add_Admin(roleMgr);
            Add_Customer(roleMgr);
            Add_Operator(roleMgr);
        }

        private static void Add_Admin(RoleManager<IdentityRole> roleMgr)
        {
            var admin = roleMgr.FindByNameAsync(RoleConstants.Admin).Result;

            if (admin is null)
            {
                admin = new IdentityRole(RoleConstants.Admin);

                var result = roleMgr.CreateAsync(admin).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("admin role created");
            }
            else
            {
                Log.Debug("admin role already exists");
            }
        }

        private static void Add_Customer(RoleManager<IdentityRole> roleMgr)
        {
            var customer = roleMgr.FindByNameAsync(RoleConstants.Customer).Result;

            if (customer is null)
            {
                customer = new IdentityRole(RoleConstants.Customer);

                var result = roleMgr.CreateAsync(customer).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("admin role created");
            }
            else
            {
                Log.Debug("admin role already exists");
            }
        }

        private static void Add_Operator(RoleManager<IdentityRole> roleMgr)
        {
            var operator_role = roleMgr.FindByNameAsync(RoleConstants.Operator).Result;

            if (operator_role is null)
            {
                operator_role = new IdentityRole(RoleConstants.Operator);
                var result = roleMgr.CreateAsync(operator_role).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("admin role created");
            }
            else
            {
                Log.Debug("admin role already exists");
            }
        }
    }
}