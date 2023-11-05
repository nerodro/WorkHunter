using DomainLayer.Models.JobHunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Property.WorkerService
{
    public interface ICVService
    {
        IEnumerable<CVModel> GetAll();
        CVModel GetCV(int id);
        void Create(CVModel cv);
        void Update(CVModel cv);
        void Delete(int id);
    }
}
