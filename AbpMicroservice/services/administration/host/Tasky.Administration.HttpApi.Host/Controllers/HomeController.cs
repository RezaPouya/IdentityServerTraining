using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.Administration.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }


    public async Task<IActionResult> UnProtected()
    {
        if (User.Identity.IsAuthenticated)
        {
            var x = "Authenticated";
        }

        return View();
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Dashboard()
    {
        return View();
    }


    [Authorize]
    public async Task<IActionResult> DashboardAuth()
    {
        return View();
    }

    public async Task LogTokenAndClaims()
    {
        var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        Debug.WriteLine($"Identity token: {identityToken}");

        foreach (var claim in User.Claims)
        {
            Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
        }
    }

    //[Authorize(Roles = "admin")]
    //public async Task<IActionResult> OnlyAdmin()
    //{
    //    var userInfo = await _movieApiService.GetUserInfo();
    //    return View(userInfo);
    //}
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }
}