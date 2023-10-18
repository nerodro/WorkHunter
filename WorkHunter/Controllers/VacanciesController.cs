using DomainLayer.Models.Vacancies;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Property.VacanciesService;
using Vacancies.Models;

namespace WorkHunter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }
        [HttpGet("GetAllResposes")]
        public IEnumerable<VacancyViewModel> GetVacancy()
        {
            List<VacancyViewModel> model = new List<VacancyViewModel>();
            if (_vacancyService != null)
            {
                _vacancyService.GetAll().ToList().ForEach(u =>
                {
                    VacancyViewModel Vacancy = new VacancyViewModel
                    {
                        Id = u.Id,
                        NameJob = u.NameJob,
                        TextJob = u.TextJob,
                    };
                    model.Add(Vacancy);
                });
            }
            return model;
        }
        [HttpPost("MakeVacancy")]
        public async Task<ActionResult<VacancyViewModel>> MakeVacancy(VacancyViewModel model)
        {
            VanancyModel cv = new VanancyModel
            {
                NameJob = model.NameJob,
                TextJob = model.TextJob,
            };
            _vacancyService.Create(cv);
            return CreatedAtAction("SingleVacancy", new { id = cv.Id }, model);
        }
        [HttpPut("EditVacancy/{id}")]
        public async Task<ActionResult<VacancyViewModel>> EditVacancy(int id, VacancyViewModel model)
        {
            model.Id = id;
            VanancyModel vacancy = _vacancyService.GetVanancy(id);
            if (ModelState.IsValid)
            {
                vacancy.NameJob = model.NameJob;
                vacancy.TextJob = model.TextJob;
                _vacancyService.Update(vacancy);
                return Ok(model);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteVacancy/{id}")]
        public async Task<ActionResult<VacancyViewModel>> DeleteVacancy(int id)
        {
            VanancyModel Vacancy = _vacancyService.GetVanancy(id);
            _vacancyService.Delete(id);
            return Ok(Vacancy);
        }
        [HttpGet("SingleVacancy/{id}")]
        public async Task<ActionResult<VanancyModel>> SingleVacancy(int id)
        {
            VacancyViewModel model = new VacancyViewModel();
            if (id != 0)
            {
                VanancyModel vacancy = _vacancyService.GetVanancy(id);
                model.NameJob = vacancy.NameJob;
                model.TextJob = vacancy.TextJob;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
