using DomainLayer.Models.Role;
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
        public DbSet<CompanyContext> Companies { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
