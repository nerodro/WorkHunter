using Microsoft.AspNetCore.Mvc;

namespace JobGiver.Models
{
    public class CompanyViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompanyDescription { get; set; } = null!;
        public string CompanyPhone { get; set; } = null!;
        public string CompanyEmail { get; set; } = null!;
        public int CompanyStatus { get; set; }
    }
}
