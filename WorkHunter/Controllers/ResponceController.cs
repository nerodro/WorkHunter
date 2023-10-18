using DomainLayer.Models.JobHunter;
using DomainLayer.Models.Vacancies;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Property.VacanciesService;
using Vacancies.Models;

namespace WorkHunter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResponceController : ControllerBase
    {
        private readonly IResponceService _responceService;
        public ResponceController(IResponceService responceService)
        {
            _responceService = responceService;
        }
        [HttpGet("GetAllResposes")]
        public IEnumerable<ResponseViewModel> GetResponse()
        {
            List<ResponseViewModel> model = new List<ResponseViewModel>();
            if (_responceService != null)
            {
                _responceService.GetAll().ToList().ForEach(u =>
                {
                    ResponseViewModel response = new ResponseViewModel
                    {
                        Id = u.Id,
                        VacancyId = u.VacancyId
                    };
                    model.Add(response);
                });
            }
            return model;
        }
        [HttpPost("MakeResponse")]
        public async Task<ActionResult<ResponseViewModel>> MakeResponse(ResponseViewModel model)
        {
            ResponseModel cv = new ResponseModel
            {
                VacancyId=model.VacancyId,
            };
            _responceService.Create(cv);
            return CreatedAtAction("SingleResponse", new { id = cv.Id }, model);
        }
        [HttpPut("EditResponse/{id}")]
        public async Task<ActionResult<ResponseViewModel>> EditResponse(int id, ResponseViewModel model)
        {
            model.Id = id;
            ResponseModel response = _responceService.GetResponse(id);
            if (ModelState.IsValid)
            {
                response.VacancyId = model.VacancyId;
                _responceService.Update(response);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteResponse/{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteResponse(int id)
        {
            ResponseModel response = _responceService.GetResponse(id);
            _responceService.Delete(id);
            return Ok(response);
        }
        [HttpGet("SingleRespnse/{id}")]
        public async Task<ActionResult<ResponseModel>> SingleResponse(int id)
        {
            ResponseViewModel model = new ResponseViewModel();
            if (id != 0)
            {
                ResponseModel response = _responceService.GetResponse(id);
                response.VacancyId = model.VacancyId;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
