using DomainLayer.Models.Vacancies;
using RepositoryLayer.Infrastructure.Vanancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.VacanciesService
{
    public class ResponceService : IResponceService
    {
        private IResponseLogic<ResponseModel> _response;
        public ResponceService(IResponseLogic<ResponseModel> response)
        {
            _response = response;
        }
        public IEnumerable<ResponseModel> GetAll()
        {
            return _response.GetAll();
        }
        public ResponseModel GetResponse(int id)
        {
            return _response.Get(id);
        }
        public void Create(ResponseModel response)
        {
            _response.Create(response);
        }
        public void Update(ResponseModel response)
        {
            _response.Update(response);
        }
        public void Delete(int id)
        {
            ResponseModel response = GetResponse(id);
            _response.Delete(response);
            _response.SaveChanges();
        }
    }
}
