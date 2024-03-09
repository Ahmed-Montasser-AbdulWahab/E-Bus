using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.ReservationDTO;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class TripsController : Controller
    {
        private readonly ITripsService _tripsService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TripsController(ITripsService tripsService, IReservationService reservationService, UserManager<ApplicationUser> userManager)
        {
            _tripsService = tripsService;
            _reservationService = reservationService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> UpcomingTrips()
        {
            return View(await _tripsService.GetUpcomingTripsAsync());
        }

        
    }
}
