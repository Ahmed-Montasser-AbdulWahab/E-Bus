using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



    }
}
