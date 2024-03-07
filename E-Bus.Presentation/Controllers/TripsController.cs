using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class TripsController : Controller
    {
        private readonly ITripsService _tripsService;

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UpcomingTrips()
        {
            return View(await _tripsService.GetUpcomingTripsAsync());
        }
    }
}
