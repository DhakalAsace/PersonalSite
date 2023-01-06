using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PortfolioProject.Areas.Admin.Models
{
    public class ProfileModel
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        
        




    }
}
