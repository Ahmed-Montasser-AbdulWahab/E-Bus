using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class ErrorsController : Controller
    {
        public async Task<IActionResult> ShowError(int statusCode)
        {
            if (statusCode == 500)
            {
                return View();
            } else if (statusCode == 404)
            {
                return View("ShowNotFound");
            }
            else
            {
                return View("ShowBadRequest");
            }
        }

    }
}
