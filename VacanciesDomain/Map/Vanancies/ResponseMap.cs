using DomainLayer.Models.Vacancies;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.Vanancies
{
    public class ResponseMap
    {
        public ResponseMap(EntityTypeBuilder<ResponseModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            //entityTypeBuilder.Property(t => t.UserId).IsRequired();
            entityTypeBuilder.Property(t => t.VanancyId).IsRequired();
        }
    }
}
