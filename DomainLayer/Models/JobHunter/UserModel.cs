using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Role;
using DomainLayer.Models.Vacancies;

namespace DomainLayer.Models.JobHunter
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int status { get; set; } = 0!;
       // public virtual CVModel CVModel { get; set; }
        public int RoleId { get; set; } = 0!;
        public RoleModel Role { get; set; }
        //public virtual List<ResponseModel> Responses { get; set; }
        //public UserModel() 
        //{
        //    Responses = new List<ResponseModel>();
        //}
        //public virtual Role Role { get; set; }
    }
}
