using DomainLayer.Models.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructure.Vanancies
{
    public interface IVananciesLogic<T> where T : VanancyModel
    {
        IEnumerable<T> GetAll();
        T GetCompany(long id);
        T Get(long id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
        void RemoveAll(List<T> entity);
    }
}
