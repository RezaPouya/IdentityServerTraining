using InvoiceService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace InvoiceService.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CallOrderService()
        {
            var result = await ApiCallerHelper.CallingApi();
            return Ok(result);
        }
    }
}