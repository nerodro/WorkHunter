using DomainLayer.Models.JobHunter;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Property.WorkerService;
using User.Models;

namespace PersonsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CVController : ControllerBase
    {
        private readonly ICVService _cvService;
        public CVController(ICVService cvService) 
        { 
            _cvService = cvService;
        }
        [HttpGet("GetAllcvs")]
        public IEnumerable<CViewModel> Getcvs()
        {
            List<CViewModel> model = new List<CViewModel>();
            if (_cvService != null)
            {
                _cvService.GetAll().ToList().ForEach(u =>
                {
                    CViewModel cv = new CViewModel
                    {
                        Id = u.Id,
                        LongText = u.LongText,
                        WantedJob = u.WantedJob
                    };
                    model.Add(cv);
                });
            }
            return model;
        }
        [HttpPost("Createcv")]
        public async Task<ActionResult<CViewModel>> Createcv(CViewModel model)
        {
            CVModel cv = new CVModel
            {
                LongText = model.LongText,
                WantedJob = model.WantedJob,    
            };
            _cvService.Create(cv);
            return CreatedAtAction("Singlecv", new { id = cv.Id }, model);
        }
        [HttpPut("Editcv/{id}")]
        public async Task<ActionResult<CViewModel>> Editcv(int id, CViewModel model)
        {
            model.Id = id;
            CVModel cv = _cvService.GetCV(id);
            if (ModelState.IsValid)
            {
                cv.WantedJob = model.WantedJob;
                cv.LongText = model.LongText;
                _cvService.Update(cv);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("Deletecv/{id}")]
        public async Task<ActionResult<CViewModel>> Deletecv(int id)
        {
            CVModel cv = _cvService.GetCV(id);
            _cvService.Delete(id);
            return Ok(cv);
        }
        [HttpGet("Singlecv/{id}")]
        public async Task<ActionResult<CVModel>> Singlecv(int id)
        {
            CViewModel model = new CViewModel();
            if (id != 0)
            {
                CVModel cv = _cvService.GetCV(id);
                cv.LongText = model.LongText;
                cv.WantedJob = model.WantedJob;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
