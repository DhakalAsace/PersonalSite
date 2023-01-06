using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Areas.Admin.Models;
using System.Security.Claims;

namespace PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "AdminHome");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {

            if (modelLogin.Email == "#" &&
                modelLogin.PassWord == "#"
                )
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("","")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "AdminHome");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}
