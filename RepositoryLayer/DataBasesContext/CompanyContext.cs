using DomainLayer.Maps.Company;
using DomainLayer.Maps.Role;
using DomainLayer.Maps.Vanancies;
using DomainLayer.Models.Company;
using DomainLayer.Models.Role;
using DomainLayer.Models.Vacancies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBasesContext
{
    public class CompanyContext : DbContext
    {
        public DbSet<CompanyModel> Companies { get; set; }
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CompanyMap(modelBuilder.Entity<CompanyModel>());
        }
    }
}
