using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class ErrorsController : Controller
    {
        public IActionResult ShowError()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            ViewBag.Error = (error != null) ? error.Error.Message : "Exception Occurred";

            return View();
        }

        //public IActionResult ShowNotFound()
        //{
        //    return View();
        //}
    }
}
