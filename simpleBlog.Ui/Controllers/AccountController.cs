using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using simpleBlog.Ui.Interface;
using simpleBlog.Ui.Models;
using simpleBlog.Ui.Repository;

namespace simpleBlog.Ui.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<LoginModel.Response> userRepo;

        public AccountController(IConfigUi config)
        {
            userRepo = new AccountRepository(config);
        }
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string username, string password, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            IEnumerable<LoginModel.Response> dataUser = await userRepo.GetData(username, password);
            if (dataUser != null)
            {
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,username),
                        new Claim(ClaimTypes.Name, dataUser.FirstOrDefault()?.fullname ?? "Unknown User"),
                        new Claim(ClaimTypes.Role, dataUser.FirstOrDefault()?.role ?? string.Empty),
                        new Claim("token",dataUser.FirstOrDefault()?.token ?? string.Empty),
                        new Claim("refresh_token",dataUser.FirstOrDefault()?.refresh_token ?? string.Empty)
                        //,new Claim(ClaimTypes.Role, "Admin")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }
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
