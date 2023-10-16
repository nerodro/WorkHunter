using DomainLayer.Models.Company;
using JobGiver.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Property.CompanyService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobGiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _company = null!;
        public CompanyController(ICompanyService company)
        {
            _company = company;
        }
        [HttpGet("GetAllCompanys")]
        public IEnumerable<CompanyViewModel> AllCompanys()
        {
            List<CompanyViewModel> model = new List<CompanyViewModel>();
            if (_company != null)
            {
                _company.GetCompanys().ToList().ForEach(u =>
                {
                    CompanyViewModel modelModel = new CompanyViewModel
                    {
                        Id = u.Id,
                        CompanyName = u.CompanyName,
                        CompanyDescription = u.CompanyDescription,
                        CompanyEmail = u.CompanyEmail,
                        CompanyPhone = u.CompanyPhone,
                        CompanyStatus = u.CompanyStatus,
                    };
                    model.Add(modelModel);
                });
            }
            return model;
        }
        [HttpPost("CreateCompany")]
        public ActionResult AddCompay(CompanyViewModel model)
        {
            CompanyModel company = new CompanyModel
            {
                CompanyName = model.CompanyName,
                CompanyDescription = model.CompanyDescription,
                CompanyEmail = model.CompanyEmail,
                CompanyPhone = model.CompanyPhone,
                CompanyStatus = model.CompanyStatus,
            };
            _company.CreateCompany(company);
            if (company.Id != 0)
            {
                return CreatedAtAction("CurrentCompany", new { id = company.Id }, company);
            }
            return BadRequest();
        }
        [HttpPut("EditCompany/{id}")]
        public async Task<ActionResult<CompanyViewModel>> EditCompany(int id, CompanyViewModel model)
        {
            model.Id = id;
            CompanyModel company = _company.GetCompany(id);
            if (company != null)
            {
                company.CompanyName = model.CompanyName;
                company.CompanyDescription = model.CompanyDescription;
                company.CompanyEmail = model.CompanyEmail;
                company.CompanyPhone = model.CompanyPhone;
                company.CompanyStatus = model.CompanyStatus;
            }
            _company.UpdateCompany(company);
            if (company.Id != 0)
            {
                return CreatedAtAction("CurrentCompany", new { id = company.Id }, company);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteCompany/{id}")]
        public ActionResult DeleteCompany(int id)
        {
            CompanyModel company = _company.GetCompany(id);
            if (company != null)
            {
                _company.DeleteCompany(id);
                return Ok(company);
            }
            return BadRequest();
        }
        [HttpGet("GetCurrentCompany/{id}")]
        public ActionResult CurrentCompany(int id)
        {
            CompanyViewModel model = new CompanyViewModel();
            if (id != 0)
            {
                CompanyModel company = _company.GetCompany(id);
                model.CompanyName = company.CompanyName;
                model.CompanyDescription = company.CompanyDescription;
                model.CompanyEmail = company.CompanyEmail;
                model.CompanyPhone = company.CompanyPhone;
                model.CompanyStatus = company.CompanyStatus;
                model.CompanyName = company.CompanyName;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
