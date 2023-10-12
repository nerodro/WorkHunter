using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Role;
using RepositoryLayer.Infrastructure.Role;
using RepositoryLayer.Infrastructure.Worker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.RoleService
{
    public class RoleService : IRoleService
    {
        private IRoleLogic<RoleModel> _roles;
        private UserLogic<UserModel> _users;
        public RoleService(IRoleLogic<RoleModel> roles, UserLogic<UserModel> users)
        {
            _roles = roles;
            _users = users;
        }
        public IEnumerable<RoleModel> GetAll()
        {
            return _roles.GetAll();
        }
        public IEnumerable<UserModel> GetUserForRole(long id)
        {
            yield return (UserModel)_users.GetAll().Where(x => x.RoleId == id);
        }

        public RoleModel Get(long id)
        {
            return _roles.Get(id);
        }
    }
}
