using CompanyService.Property.CompanyService;
using Company.RabitMQ;
using CompanyDomain.Models.Company;
using JobGiver.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Vacancies.Models;

namespace JobGiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _company = null!;
        private readonly IRabitMQListener _rabitMQListen;
        private readonly IRabitMQProducer _rabitMQProducer;
        private readonly IModel _rabbitMqChannel;
        public CompanyController(ICompanyService company, IRabitMQListener rabitMQ, IRabitMQProducer rabitMQProducer)
        {
            _company = company;
            _rabitMQListen = rabitMQ;
            _rabitMQProducer = rabitMQProducer;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _rabbitMqChannel = connection.CreateModel();

            // Создание очереди для отправки сообщений
            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);

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
        [HttpGet("GetVacanciesForCompan/{id}")]
        public IEnumerable<VacancyViewModel> GetVacanciesForCompany(int id)
        {
            var request = new VacancyViewModel
            {
                CompanyId = id,
            };
            var requestJson = JsonConvert.SerializeObject(request);
            var message = $"CompanyId:{id}";
            var correlationId = Guid.NewGuid().ToString();
            var body = Encoding.UTF8.GetBytes(requestJson);
            var responseQueueName = _rabbitMqChannel.QueueDeclare().QueueName;

            var properties = _rabbitMqChannel.CreateBasicProperties();
            properties.ReplyTo = responseQueueName;
            properties.CorrelationId = correlationId;
            _rabbitMqChannel.BasicPublish("", "vacancy_requests", properties, body);
            var responseWaiter = new ManualResetEventSlim(false);

            List<VacancyViewModel> modelvac = new List<VacancyViewModel>();
            var consumer = new EventingBasicConsumer(_rabbitMqChannel);
            consumer.Received += (model, ea) =>
            {
                    var responseMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var modelresp = JsonConvert.DeserializeObject<VacancyViewModel>(responseMessage);
                    modelvac.Add(modelresp);
                    responseWaiter.Set(); 
            };
            _rabbitMqChannel.BasicConsume("company_vacancies_response_queue", true, consumer);
            if (!responseWaiter.Wait(TimeSpan.FromSeconds(10)))
            {
                // Если ответ не получен в течение указанного времени, возвращаем ошибку
               // return InternalServerError(new Exception("Timeout waiting for vacancies response"));
            }
            return modelvac;
        }

        private Task<List<VacancyViewModel>> InternalServerError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
