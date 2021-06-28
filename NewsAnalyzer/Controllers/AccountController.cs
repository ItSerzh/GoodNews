using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Models.ViewModels.Account;
using NewsAnalyzer.Util.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwHash = Text.EncryptSHA256(model.Password);
                var userRegistered = await _userService.Register(new UserDto
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email,
                    PasswordHash = passwHash,
                });

                if (userRegistered)
                {
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    return BadRequest();
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var targetUser = await _userService.GetByEmail(model.Email);
                
                if(targetUser != null)
                {
                    var passwHash = Text.EncryptSHA256(model.Password);
                    if (targetUser.PasswordHash.Equals(passwHash))
                    {
                        await Authenticate(targetUser);
                        return RedirectToAction("Index", "Home");
                    }
                }
             }
            return View(model);
        }

        private async Task Authenticate(UserDto user)
        {
            var authType = "ApplicationCookie";

            var nameClaim = new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email);
            nameClaim.Properties.Add("FirstName", user.FirstName);
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, (await _roleService.GetUserRole(user.Email)).Name)
            };

            var identity = new ClaimsIdentity(claims,
                authType, 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
