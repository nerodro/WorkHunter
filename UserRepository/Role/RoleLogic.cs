using DomainLayer.Models.Role;
using Microsoft.EntityFrameworkCore;
using CompanyRepository.DataBasesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyRepository.Infrastructure.Role
{
    public class RoleLogic<T> : IRoleLogic<T> where T : RoleModel
    {
        private readonly WorketContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public RoleLogic(WorketContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<T> GetUsersForRole(long id)
        {
            return entities.AsEnumerable();
        }

        public T Get(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }
    }
}
