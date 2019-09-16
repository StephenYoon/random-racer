using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsApi.Models;

namespace MetricsApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/login")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, userLogin.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return Ok($"User logged in: {userLogin.Email}");
            }
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            //}
            //if (result.IsLockedOut)
            //{
            //    _logger.LogWarning("User account locked out.");
            //    return RedirectToAction(nameof(Lockout));
            //}
            else
            {
                _logger.LogInformation($"Invalid login attempt: {userLogin.Email}");
                return BadRequest("Invalid login attempt.");
            }
        }

        [HttpPost]
        [Route("/api/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Ok("User logged out.");
        }
    }
}
