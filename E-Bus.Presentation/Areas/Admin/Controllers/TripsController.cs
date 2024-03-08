using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.TripDTO;

namespace E_Bus.Presentation.Areas.Admin.Controllers
{



    [Route("[area]/[controller]/[action]")]
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class TripsController : Controller
    {
        private readonly ITransportationsService transportationsService;
        private readonly ITripsService tripService;



        public TripsController(ITransportationsService transportationsService, ITripsService tripService)
        {
            this.transportationsService = transportationsService;
            this.tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> AddTrip()
        {
            var transportations = await transportationsService.GetAllAsync();
            
            if(transportations is null) { /* Redirect to create Transportation*/ }

            ViewBag.Means = transportations.Select(t => new SelectListItem() { Text=t.Name , Value= t.Id.ToString()});


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrip(TripAddRequest tripAddRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage.ToString());
                var errorsString = string.Join("\n-", errors);
                ViewBag.Errors = errorsString;
                var transportations = await transportationsService.GetAllAsync();

                if (transportations is null) { /* Redirect to create Transportation*/ }

                ViewBag.Means = transportations.Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() });
                return View(tripAddRequest);
            }

            if (!await tripService.AddTripAsync(tripAddRequest))
            {
                ViewBag.Errors = "Trip failed to be saved.";
                var transportations = await transportationsService.GetAllAsync();

                if (transportations is null) { /* Redirect to create Transportation*/ }

                ViewBag.Means = transportations.Select(t => new SelectListItem() { Text = t.Name, Value = t.Id.ToString() });
                return View(tripAddRequest);
            }

            return RedirectToAction(actionName: "UpcomingTrips", controllerName: "Trips", routeValues: new { area = null as string});
        }

    }
}
