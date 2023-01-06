using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioProject.Areas.Admin.Data;
using PortfolioProject.Areas.Admin.Models;
using PortfolioProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioProject.Areas.Admin.Controllers
{
    [Authorize]

    [Area("Admin")]

    public class AdminHome : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminHome(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // GET: Admin/AdminHome
       


       
    }
}
