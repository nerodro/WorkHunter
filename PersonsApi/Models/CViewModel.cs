using Microsoft.AspNetCore.Mvc;

namespace User.Models
{
    public class CViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string WantedJob { get; set; } = null!;
        public string LongText { get; set; } = null!;
    }
}
