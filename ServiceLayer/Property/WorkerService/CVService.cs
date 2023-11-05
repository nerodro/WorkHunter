using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Vacancies;
using CompanyRepository.Infrastructure.Vanancies;
using CompanyRepository.Infrastructure.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.WorkerService
{
    public class CVService : ICVService
    {
        private ICVLogic<CVModel> _cv;
        public CVService(ICVLogic<CVModel> cv)
        {
            _cv = cv;
        }
        public IEnumerable<CVModel> GetAll()
        {
            return _cv.GetAll();
        }
        public CVModel GetCV(int id)
        {
            return _cv.GetCV(id);
        }
        public void Create(CVModel cv)
        {
            _cv.Create(cv);
        }
        public void Update(CVModel cv)
        {
            _cv.Update(cv);
        }
        public void Delete(int id)
        {
            CVModel cv = GetCV(id);
            _cv.Delete(cv);
            _cv.SaveChanges();
        }
    }
}
