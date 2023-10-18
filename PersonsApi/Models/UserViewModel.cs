using Microsoft.AspNetCore.Mvc;

namespace User.Models
{
    public class UserViewModel
    {
        [HiddenInput]
        public Int64 Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int status { get; set; } = 0!;
        public int RoleId { get; set; } = 0!;
    }
}
