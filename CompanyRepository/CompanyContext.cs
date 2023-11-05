using CompanyDomain.Maps.Company;
using CompanyDomain.Models.Company;
using Microsoft.EntityFrameworkCore;

namespace CompanyRepository.DataBasesContext
{
    public class CompanyContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; }
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            new CompanyMap(modelBuilder.Entity<CompanyModel>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
