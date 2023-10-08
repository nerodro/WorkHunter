using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Role;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.Role
{
    public class RoleMap
    {
        public RoleMap(EntityTypeBuilder<RoleModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.RoleName).IsRequired();
            entityTypeBuilder.Property(x => x.status).IsRequired();
            entityTypeBuilder.Property(x => x.Email).IsRequired();
            entityTypeBuilder.Property(x => x.Password).IsRequired();
            entityTypeBuilder.Property(x => x.PhoneNumber).IsRequired();
            entityTypeBuilder.Property(x => x.PhoneNumber).IsRequired();
        }
    }
}
