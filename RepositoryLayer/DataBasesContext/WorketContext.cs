using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Role;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DataBasesContext
{
    public class WorketContext : DbContext
    {
        public DbSet<CVModel> CV { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public WorketContext(DbContextOptions<WorketContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
