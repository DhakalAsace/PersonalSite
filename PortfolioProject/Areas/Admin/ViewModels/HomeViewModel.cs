using NuGet.DependencyResolver;
using PortfolioProject.Areas.Admin.Controllers;
using PortfolioProject.Areas.Admin.Models;

namespace PortfolioProject.Areas.Admin.ViewModels
{
    public class HomeViewModel
    {

        public IEnumerable<ProfileModel> ProfileModels { get; set; }
        public IEnumerable<ProjectsModel> ProjectsModels { get; set; }
        public IEnumerable<AboutModel> AboutModels { get; set; }


    }
}
