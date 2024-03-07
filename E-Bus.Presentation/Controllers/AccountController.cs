using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;

namespace E_Bus.Presentation.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Authorize("NoLogin")]
        public async Task<IActionResult> Register()
        { 
            return View();
        }
        [HttpPost]
        [Authorize("NoLogin")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage.ToString());
                var errorsString = string.Join("\n-", errors);
                ViewBag.Errors = errorsString;
                return View(registerDTO);
            }

            ApplicationUser user = registerDTO.ToApplicationUser();
            IdentityResult userResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (userResult.Succeeded)
            {
                var roleName = UserType.User.ToString();
                if (await _roleManager.FindByNameAsync(roleName) is null)
                {
                    ApplicationRole role = new ApplicationRole() { Name = roleName };
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded)
                    {
                        await _userManager.DeleteAsync(user);
                        var error = roleResult.Errors.Select(e => e.Description);
                        ViewBag.Errors = string.Join("\n", error);
                        return View(registerDTO);
                    }
                }
                var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!addToRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    var error = addToRoleResult.Errors.Select(e => e.Description);
                    ViewBag.Errors = string.Join("\n", error);
                    return View(registerDTO);
                }

                return RedirectToAction(actionName: "Login", controllerName: "Account");
            }
            else
            {
                var error = userResult.Errors.Select(e => e.Description);
                ViewBag.Errors = string.Join("\n", error);
                return View(registerDTO);
            }
        }
        [Authorize("NoLogin")]
        public async Task<IActionResult> IsValidNationalID(string nationalID)
        {
            return Json(await _userManager.FindByNameAsync(nationalID) is null);
        }

        [HttpGet]
        [Authorize("NoLogin")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NoLogin")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, [FromQuery] string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage.ToString());
                var errorsString = string.Join("\n-", errors);
                ViewBag.Errors = errorsString;
                return View(loginDTO);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(userName:loginDTO.NationalID, password:loginDTO.Password,
                isPersistent:false, lockoutOnFailure:false);

            if(signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) )
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.Errors = "Check Credentials Again.";
                return View(loginDTO);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
