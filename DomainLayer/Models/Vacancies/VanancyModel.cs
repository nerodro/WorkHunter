using DomainLayer.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Vacancies
{
    public class VanancyModel
    {
        public int Id { get; set; }
        public string NameJob { get; set; } = null!;
        public string TextJob { get; set; } = null!;
        //public int CompanyId { get; set; } = 0!;
        //public CompanyModel Company { get; set; } = null!;
        public List<ResponseModel> Response { get; set; }
        public VanancyModel() 
        { 
            Response = new List<ResponseModel>();
        }
    }
}
