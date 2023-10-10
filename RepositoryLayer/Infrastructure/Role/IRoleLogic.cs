using DomainLayer.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructure.Role
{
    public interface IRoleLogic<T> where T : RoleModel
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetUsersForRole(long id);
        T Get(long id);
    }
}
