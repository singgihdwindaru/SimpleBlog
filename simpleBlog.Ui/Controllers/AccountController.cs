using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleBlog.Ui.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        public async Task<IActionResult> LoginAsync(string username, string password, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
          
            TempData["error"] = "Username or Password is Invalid";
            return View("Login");
        }
        [Route("/logout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
            return this.LocalRedirect("/");
        }
    }
}
