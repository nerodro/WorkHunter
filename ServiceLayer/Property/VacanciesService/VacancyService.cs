using DomainLayer.Models.Vacancies;
using RepositoryLayer.Infrastructure.Vanancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.VacanciesService
{
    public class VacancyService : IVacancyService
    {
        private IVananciesLogic<VanancyModel> _vacancy;
        public VacancyService(IVananciesLogic<VanancyModel> vanancies)
        {
            _vacancy = vanancies;
        }
        public IEnumerable<VanancyModel> GetAll()
        {
            return _vacancy.GetAll();
        }
        public VanancyModel GetVanancy(long id)
        {
            return _vacancy.GetVacancy(id);
        }
        public void Create(VanancyModel vanancy)
        {
            _vacancy.Create(vanancy);
        }
        public void Update(VanancyModel vanancy)
        {
            _vacancy.Update(vanancy);
        }
        public void Delete(int id)
        {
            VanancyModel vanancy = GetVanancy(id);
            _vacancy.Delete(vanancy);
            _vacancy.SaveChanges();
        }
        public IEnumerable<VanancyModel> GetVacanciesForCompany(long id)
        {
            return _vacancy.GetVacanciesForCompany(id);
        }
    }
}
