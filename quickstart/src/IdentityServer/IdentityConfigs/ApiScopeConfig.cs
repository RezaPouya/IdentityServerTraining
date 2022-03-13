using Duende.IdentityServer.Models;

namespace IdentityConfigs;

public static class ApiScopeConfig
{
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new ApiScope[] 
        {
            new ApiScope(name: "AccountService"   , displayName: "Account Service") ,
            new ApiScope(name: "OrderService"   , displayName: "Order Service") ,
            new ApiScope(name: "InvoiceService" , displayName: "Invoice Service") ,
        };
    }
}