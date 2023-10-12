using DomainLayer.Models.JobHunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructure.Worker
{
    public interface ICVLogic<T> where T : CVModel
    {
        IEnumerable<T> GetAll();
        T GetCV(long id);
        T Get(long id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
