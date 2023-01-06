using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Areas.Admin.Data;
using PortfolioProject.Areas.Admin.Models;

namespace PortfolioProject.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    
    public class AboutController : Controller
    {
        public readonly ApplicationDbContext _db;
        IWebHostEnvironment webHostEnvironment;

        public AboutController(ApplicationDbContext db, IWebHostEnvironment webhost)
        {
            _db = db;
            webHostEnvironment = webhost;
        }
        public IActionResult Index()
        {
            IEnumerable<AboutModel> obj = _db.about;
            return View(obj);

        }
        private string UploadedFile(AboutModel projects)
        {
            string uniquefilename = null;
            if (projects.AboutImage != null)
            {
                string uploadsfolder = Path.Combine(webHostEnvironment.WebRootPath, "customimg");
                uniquefilename = Guid.NewGuid().ToString() + "_" + projects.AboutImage.FileName;
                string filepath = Path.Combine(uploadsfolder, uniquefilename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    projects.AboutImage.CopyTo(fileStream);
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
        public IActionResult Create(AboutModel model)
        {


            if (model.AboutId == 0)
            {
                string uniquefilename = UploadedFile(model);
                model.AboutImageUrl = uniquefilename;
                _db.about.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            string uniquefilenames = UploadedFile(model);
            var data = _db.about.Where(x => x.AboutId == model.AboutId).FirstOrDefault();
            data.AboutDescription = model.AboutDescription;
            data.AboutImageUrl = uniquefilenames;
            _db.about.Update(data);
            _db.SaveChanges();
            return RedirectToAction("Index");



        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var idstoring = _db.about.Find(Id);
            return View(idstoring);
        }

        public IActionResult Delete(int Id)
        {

            var idstoring = _db.about.Find(Id);
            _db.about.Remove(idstoring);

            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
