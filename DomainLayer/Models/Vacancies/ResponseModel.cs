using DomainLayer.Models.JobHunter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Vacancies
{
    public class ResponseModel
    {
        public int Id { get; set; }
        public int VacancyId { get; set; } = 0!;
        public VanancyModel Vanancy { get; set; }
        public int UserId { get; set; } = 0!;
        public UserModel User { get; set; }
    }
}
