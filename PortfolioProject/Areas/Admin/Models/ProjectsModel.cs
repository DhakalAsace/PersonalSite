using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace PortfolioProject.Areas.Admin.Models
{
    public class ProjectsModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectGithubLink { get; set; }
        public string ProjectSiteLink { get; set; }
        public string ProjectImageUrl { get; set; }
        [NotMapped]
        public IFormFile ProjectImage { get; set; }



    }
}
