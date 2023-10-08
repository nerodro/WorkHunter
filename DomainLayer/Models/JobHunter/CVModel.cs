using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.JobHunter
{
    public class CVModel
    {
        public int Id { get; set; }
        public string WantedJob { get; set; } = null!;
        public string LongText { get; set; } = null!;
        public UserModel User { get; set; }
    }
}
