using Microsoft.AspNetCore.Mvc;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("OK");
        }
    }
}
