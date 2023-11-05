using DomainLayer.Models.JobHunter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.JobHunter
{
    public class CVMap
    {
        public CVMap(EntityTypeBuilder<CVModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.WantedJob).IsRequired();
            entityTypeBuilder.Property(x => x.LongText).IsRequired();
        }
    }
}
