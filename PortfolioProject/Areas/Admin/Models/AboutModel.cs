using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioProject.Areas.Admin.Models
{
    public class AboutModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int AboutId { get; set; }
        public string AboutDescription { get; set; }
        public string AboutImageUrl { get; set; }
        [NotMapped]
        public IFormFile AboutImage { get; set; }
    }
}
