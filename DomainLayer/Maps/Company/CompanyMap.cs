using DomainLayer.Models.Company;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.Company
{
    public class CompanyMap
    {
        public CompanyMap(EntityTypeBuilder<CompanyModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.CompanyName).IsRequired();
            entityTypeBuilder.Property(x => x.CompanyDescription).IsRequired();
            entityTypeBuilder.Property(x => x.CompanyEmail).IsRequired();
            entityTypeBuilder.Property(x => x.CompanyPhone).IsRequired();
            entityTypeBuilder.Property(x => x.CompanyStatus).IsRequired();
        }
    }
}
