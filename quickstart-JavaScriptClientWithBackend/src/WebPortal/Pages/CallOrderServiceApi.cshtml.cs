using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPortal.Helpers;


namespace MyApp.Namespace
{
    public class CallOrderServiceApiModel : PageModel
    {
        public string Result { get; private set; }
        public async Task<IActionResult> OnGet()
        {
            this.Result = await ApiCallerHelper.CallingApi();
            return null;
        }
    }
}
