using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Areas.Admin.Data;
using PortfolioProject.Areas.Admin.Models;

namespace PortfolioProject.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    
    public class ProjectsController : Controller
    {
        public readonly ApplicationDbContext _db;
        IWebHostEnvironment webHostEnvironment;

        public ProjectsController(ApplicationDbContext db, IWebHostEnvironment webhost)
        {
            _db = db;
            webHostEnvironment = webhost;
        }
        public IActionResult Index()
        {
            IEnumerable<ProjectsModel> obj = _db.projects;
            return View(obj);

        }
        private string UploadedFile(ProjectsModel projects)
        {
            string uniquefilename = null;
            if (projects.ProjectImage != null)
            {
                string uploadsfolder = Path.Combine(webHostEnvironment.WebRootPath, "customimg");
                uniquefilename = Guid.NewGuid().ToString() + "_" + projects.ProjectImage.FileName;
                string filepath = Path.Combine(uploadsfolder, uniquefilename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    projects.ProjectImage.CopyTo(fileStream);
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
        public IActionResult Create(ProjectsModel model)
        {

          
            if (model.ProjectId == 0)
            {
                string uniquefilename = UploadedFile(model);
                model.ProjectImageUrl = uniquefilename;
                _db.projects.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            string uniquefilenames = UploadedFile(model);
            var data = _db.projects.Where(x => x.ProjectId == model.ProjectId).FirstOrDefault();
            data.ProjectName = model.ProjectName;
            data.ProjectDescription = model.ProjectDescription;
            data.ProjectSiteLink = model.ProjectSiteLink;
            data.ProjectGithubLink = model.ProjectGithubLink;
            data.ProjectImageUrl = uniquefilenames;
            _db.projects.Update(data);
            _db.SaveChanges();
            return RedirectToAction("Index");



        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var idstoring = _db.projects.Find(Id);
            return View(idstoring);
        }

        public IActionResult Delete(int Id)
        {

            var idstoring = _db.projects.Find(Id);
            _db.projects.Remove(idstoring);

            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
