using DomainLayer.Models.JobHunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.WorkerService
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetUser(int id);
        void Create(UserModel user);
        void Update(UserModel user);
        void Delete(int id);
    }
}
