using DomainLayer.Models.JobHunter;
using RepositoryLayer.Infrastructure.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.WorkerService
{
    public class UserService : IUserService
    {
        private IUserLogic<UserModel> _user;
        public UserService(IU<UserModel> user)
        {
            _cv = cv;
        }
        public IEnumerable<UserModel> GetAll()
        {
            return _cv.GetAll();
        }
        public UserModel GetCV(int id)
        {
            return _cv.GetCV(id);
        }
        public void Create(UserModel cv)
        {
            _cv.Create(cv);
        }
        public void Update(UserModel cv)
        {
            _cv.Update(cv);
        }
        public void Delete(int id)
        {
            UserModel cv = GetCV(id);
            _cv.Delete(cv);
            _cv.SaveChanges();
        }
    }
}
