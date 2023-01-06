using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PortfolioProject.Areas.Admin.Data;
using PortfolioProject.Areas.Admin.Models;
using System.Net;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PortfolioProject.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
   
    public class ProfileController : Controller
    {
        public readonly ApplicationDbContext _db;
        IWebHostEnvironment webHostEnvironment;

        public ProfileController(ApplicationDbContext db, IWebHostEnvironment webhost)
        {
            _db = db;
            webHostEnvironment = webhost;
        }
        public IActionResult Index()
        {
            IEnumerable<ProfileModel> obj = _db.profiles;
            return View(obj);
        }

        private string UploadedFile(ProfileModel profiles)
        {
            string uniquefilename = null;
            if (profiles.Image != null)
            {
                string uploadsfolder = Path.Combine(webHostEnvironment.WebRootPath, "customimg");
                uniquefilename = Guid.NewGuid().ToString() + "_" + profiles.Image.FileName;
                string filepath = Path.Combine(uploadsfolder, uniquefilename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    profiles.Image.CopyTo(fileStream);
                }

            }
            return uniquefilename;
        }
        [HttpGet]
        public IActionResult Create()
        {
           
                return View();
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfileModel model)
        {


           
                if (model.ImageId == 0)
                {
                    string uniquefilename = UploadedFile(model);
                    model.ImageUrl = uniquefilename;
                    _db.profiles.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                string uniquefilenames = UploadedFile(model);
                var data = _db.profiles.Where(x => x.ImageId == model.ImageId).FirstOrDefault();
                data.ImageName = model.ImageName;
                data.Description = model.Description;
                data.ImageUrl = uniquefilenames;
                _db.profiles.Update(data);
                _db.SaveChanges();
                return RedirectToAction("Index");
            
            
            
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var idstoring = _db.profiles.Find(Id);
            return View(idstoring);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(ProfileModel model)
        //{
        //    try
        //    {



        //            string uniquefilename = UploadedFile(model);
        //            model.ImageUrl = uniquefilename;


        //        _db.profiles.Update(model);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //delete the record







        public IActionResult Delete(int Id)
        {

            var idstoring = _db.profiles.Find(Id);
            _db.profiles.Remove(idstoring);

            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }       
}
    


