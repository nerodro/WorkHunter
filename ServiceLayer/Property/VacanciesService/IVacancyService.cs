using DomainLayer.Models.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.VacanciesService
{
    public interface IVacancyService
    {
        IEnumerable<VanancyModel> GetAll();
        IEnumerable<VanancyModel> GetVacanciesForCompany(long id);
        VanancyModel GetVanancy(long id);

        void Create(VanancyModel vanancy);
        void Update(VanancyModel vanancy);
        void Delete(int id);
    }
}
