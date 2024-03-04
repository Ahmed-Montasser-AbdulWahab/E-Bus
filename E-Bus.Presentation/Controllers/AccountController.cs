﻿using E_Bus.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs;

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
        public async Task<IActionResult> Register()
        { 
            return View();
        }
        [HttpPost]
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
                var roleName = registerDTO.UserRole.ToString();
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

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            else
            {
                var error = userResult.Errors.Select(e => e.Description);
                ViewBag.Errors = string.Join("\n", error);
                return View(registerDTO);
            }
        }

        public async Task<IActionResult> IsValidNationalID(string nationalID)
        {
            return Json(await _userManager.FindByNameAsync(nationalID) is null);
        }
    }
}
