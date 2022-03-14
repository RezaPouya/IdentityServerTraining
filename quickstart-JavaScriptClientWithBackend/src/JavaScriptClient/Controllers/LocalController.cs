using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JavaScriptClient.Controllers
{
    public class LocalController : Controller
    {
        [Route("local/identity")]
        [Authorize]
        public IResult LocalIdentityHandler(ClaimsPrincipal user)
        {

            var name = user.FindFirst("name")?.Value ?? user.FindFirst("sub")?.Value;
            return Results.Json(new { message = "Local API Success!", user = name });
        }

    }
}
