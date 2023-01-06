using Microsoft.EntityFrameworkCore;
using PortfolioProject.Areas.Admin.Models;

namespace PortfolioProject.Areas.Admin.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProfileModel> profiles { get; set; }
        public DbSet<ProjectsModel> projects { get; set; }
        public DbSet<AboutModel> about { get; set; }    
    }
}
