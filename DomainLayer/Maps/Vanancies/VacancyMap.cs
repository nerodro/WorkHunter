using DomainLayer.Models.Vacancies;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.Vanancies
{
    public class VacancyMap
    {
        public VacancyMap(EntityTypeBuilder<VanancyModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(t => t.NameJob).IsRequired();
            entityTypeBuilder.Property(t => t.TextJob).IsRequired();
            //entityTypeBuilder.Property(t => t.CompanyId).IsRequired();
        }
    }
}
