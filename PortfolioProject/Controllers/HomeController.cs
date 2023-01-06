using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioProject.Areas.Admin.Data;
using PortfolioProject.Areas.Admin.Models;
using PortfolioProject.Areas.Admin.ViewModels;
using PortfolioProject.Models;
using System.Diagnostics;
using System.Dynamic;

namespace PortfolioProject.Controllers
{
    public class HomeController : Controller
    {
      
        public readonly ApplicationDbContext _db;
        IWebHostEnvironment webHostEnvironment;
        public HomeController(ApplicationDbContext db, IWebHostEnvironment webhost)
        {
            _db = db;
            webHostEnvironment = webhost;
        }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel= new HomeViewModel();
            homeViewModel.ProfileModels = _db.profiles.ToList();
            homeViewModel.ProjectsModels = _db.projects.ToList();
            homeViewModel.AboutModels = _db.about.ToList();
            return View(homeViewModel);

        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}