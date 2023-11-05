using DomainLayer.Models.Vacancies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRepository.DataBasesContext
{
    public class VacancyContext : DbContext
    {
        public DbSet<ResponseModel> Responses { get; set; }
        public DbSet<VanancyModel> Vanancy { get; set; }
        public VacancyContext(DbContextOptions<VacancyContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
