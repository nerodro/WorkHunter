using DomainLayer.Models.Vacancies;
using Microsoft.AspNetCore.Mvc;

namespace Vacancies.Models
{
    public class VacancyViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string NameJob { get; set; } = null!;
        public string TextJob { get; set; } = null!;
        public int CompanyId { get; set; } = 0!;
    }
}
