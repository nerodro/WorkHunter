using DomainLayer.Models.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.VacanciesService
{
    public interface IResponceService
    {
        IEnumerable<ResponseModel> GetAll();
        ResponseModel GetResponse(int id);
        void Create(ResponseModel response);
        void Update(ResponseModel response);
        void Delete(int id);
    }
}
