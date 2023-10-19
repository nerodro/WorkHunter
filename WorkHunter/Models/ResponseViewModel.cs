using Microsoft.AspNetCore.Mvc;

namespace Vacancies.Models
{
    public class ResponseViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public int VacancyId { get; set; } = 0!;
    }
}
