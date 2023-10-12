using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Role;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.RoleService
{
    public interface IRoleService
    {
        IEnumerable<RoleModel> GetAll();
        IEnumerable<UserModel> GetUserForRole(long id);
        RoleModel Get(long id);
    }
}
