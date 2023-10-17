using DomainLayer.Models.JobHunter;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Property.WorkerService;
using User.Models;

namespace PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService = null!;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUsers")]
        public IEnumerable<UserViewModel> GetUsers()
        {
            List<UserViewModel> model = new List<UserViewModel>();
            if(_userService != null)
            {
                _userService.GetAll().ToList().ForEach(u =>
                {
                    UserViewModel user = new UserViewModel 
                    { 
                        Id = u.Id,
                        RoleId = u.RoleId,
                        PhoneNumber = u.PhoneNumber,
                        Email = u.Email,
                        UserName = u.UserName,
                        status = u.status
                    };
                    model.Add(user);
                });
            }
            return model;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserViewModel model)
        {
            UserModel user = new UserModel
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                RoleId = model.RoleId,
                status = model.status
            };
            _userService.Create(user);
            return CreatedAtAction("SingleUser", new {id = user.Id}, model);
        }
        [HttpPut("EditUser/{id}")]
        public async Task<ActionResult<UserViewModel>> EditUser(int id, UserViewModel model)
        {
            model.Id = id;
            UserModel user = _userService.GetUser(id);
            if(ModelState.IsValid)
            {
                user.Email = model.Email;
                user.Password = model.Password;
                user.PhoneNumber = model.PhoneNumber;
                user.RoleId = model.RoleId;
                user.status = model.status;
                user.UserName = model.UserName;
                _userService.Update(user);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<UserViewModel>> DeleteUser(int id)
        {
            UserModel user = _userService.GetUser(id);
            _userService.Delete(id);
            return Ok(user);
        }
        [HttpGet("SingleUser/{id}")]
        public async Task<ActionResult<UserModel>> SingleUser(int id)
        {
            UserViewModel model = new UserViewModel();
            if(id != 0)
            {
                UserModel user = _userService.GetUser(id);
                model.Email = user.Email;
                model.Password = user.Password;
                model.PhoneNumber = user.PhoneNumber;
                model.RoleId = user.RoleId;
                model.Id = user.Id;
                model.status = user.status;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
