using DomainLayer.Models.JobHunter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Maps.JobHunter
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<UserModel> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.UserName).IsRequired();
            entityTypeBuilder.Property(x => x.status).IsRequired();
            entityTypeBuilder.Property(x => x.Email).IsRequired();
            entityTypeBuilder.Property(x => x.Password).IsRequired();
            entityTypeBuilder.Property(x => x.PhoneNumber).IsRequired();
            entityTypeBuilder.Property(x => x.PhoneNumber).IsRequired();
            entityTypeBuilder.Property(x => x.RoleId).IsRequired();
        }
    }
}
