using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class ErrorsController : Controller
    {
        public async Task<IActionResult> ShowError()
        {
            return View();
        }

        //public IActionResult ShowNotFound()
        //{
        //    return View();
        //}
    }
}
