using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTOs.ReservationDTO;
using ServiceContracts.DTOs;

namespace E_Bus.Presentation.Areas.User.Controllers
{
    [Route("[area]/[controller]/[action]")]
    [Area("user")]
    [Authorize(Roles = "User")]
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

        [HttpGet("{tripId:guid}")]
        public async Task<IActionResult> RegisterTrip(Guid tripId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return StatusCode(400);
            }
            if (await _reservationService.GetReservationById($"{tripId}{user.Id}", false) is not null)
            {
                ViewBag.Errors = "You can't register more than once for same trip.";
                return View();
            }
            ReservationResponse userTripWrapperModel = new ReservationResponse()
            {
                TheTripResponse = await _tripsService.GetTripByIdAsync(tripId, true),
                TheUserDTO = user.ToUserDTO()
            };

            if (userTripWrapperModel.TheTripResponse is null || userTripWrapperModel.TheUserDTO is null) { return StatusCode(400); }

            return View(userTripWrapperModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPostTrip([FromForm] ReservationAddRequest reservationAddRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage.ToString());
                var errorsString = string.Join("\n-", errors);
                ViewBag.Errors = errorsString;
                return RedirectToAction(nameof(RegisterTrip), "Trips", routeValues: new { area = "user", tripId = reservationAddRequest.TripId });
            }

            if (!await _reservationService.AddReservationAsync(reservationAddRequest))
            {

                return RedirectToAction(nameof(RegisterTrip), "Trips", routeValues: new { area = "user", tripId = reservationAddRequest.TripId });
            }
            //Go to Ticket Page
            return RedirectToAction(actionName: nameof(ShowReservation), controllerName: "Trips", routeValues: new { area = "user", tripId = reservationAddRequest.TripId, userId = reservationAddRequest.UserId });
        }

        [HttpGet("{TripId:guid}/{UserId:guid}")]
        public async Task<IActionResult> ShowReservation([FromRoute] ReservationId reservationId)
        {
            var userTripWrapperModel = await _reservationService.GetReservationById($"{reservationId.TripId}{reservationId.UserId}");

            if (userTripWrapperModel is null || userTripWrapperModel.TheTripResponse is null || userTripWrapperModel.TheUserDTO is null) { return StatusCode(400); }

            return View(userTripWrapperModel);
        }

        [HttpGet("{TripId:guid}/{UserId:guid}")]
        public async Task<IActionResult> ShowPdf([FromRoute] ReservationId reservationId)
        {
            var userTripWrapperModel = await _reservationService.GetReservationById($"{reservationId.TripId}{reservationId.UserId}");

            if (userTripWrapperModel is null || userTripWrapperModel.TheTripResponse is null || userTripWrapperModel.TheUserDTO is null) { return StatusCode(400); }

            return new ViewAsPdf(viewName: "ShowPdf", viewData: ViewData, model: userTripWrapperModel)
            {
                ContentType = "application/pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 3,
                    Bottom = 3,
                    Left = 3,
                    Right = 3
                }
            };
        }

        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userName = User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName)) return StatusCode(StatusCodes.Status400BadRequest);
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var userReservations = await _reservationService.GetReservationByUserId(user.Id);

            return View(userReservations);
        }

        [HttpPost("{TripId:guid}/{UserId:guid}")]
        [HttpGet("{TripId:guid}/{UserId:guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] ReservationId reservationId,[FromForm] DateTime DepartureTime)
        {
            if (DepartureTime.Equals(new DateTime()) || DateTime.Now.CompareTo(DepartureTime.AddDays(-1)) > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return (await _reservationService.DeleteReservation(reservationId)) ?
                RedirectToAction(actionName:nameof(MyReservations), controllerName:"Trips", routeValues: new { area = "user" })
            : StatusCode(StatusCodes.Status400BadRequest)
            ;
        }
    }
}
