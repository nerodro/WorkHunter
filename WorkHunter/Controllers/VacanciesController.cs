using DomainLayer.Models.Vacancies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLayer.Property.VacanciesService;
using System.Text;
using System.Threading.Channels;
using Vacancies.Models;
using Vacancies.RabitMQ;

namespace WorkHunter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IRabitMQProducer _rabbitProducer;
        private readonly IConnection _rabbitMqConnection;
        private readonly IModel _rabbitMqChannel;
        public VacanciesController(IVacancyService vacancyService, IRabitMQProducer rabitMQ)
        {
            _vacancyService = vacancyService;
            _rabbitProducer = rabitMQ;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqConnection = factory.CreateConnection();
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();

            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);

            var consumer = new EventingBasicConsumer(_rabbitMqChannel);
            VanancyModel modelvac = new VanancyModel()
            {
                Id = 1,
                TextJob = "Test",
                NameJob = "Test",
                CompanyId = 1,
            };
            consumer.Received += (model, ea) =>
            {
                //var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                //var companyId = int.Parse(message.Split(':')[1]);
                //int id = 0;
                //id = companyId;
                var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Десериализуем запрос из JSON
                var request = JsonConvert.DeserializeObject<VacancyViewModel>(requestJson);

                // Выполняем запрос в базе данных для получения списка вакансий для компании
                //var vacancies = _vacancyService.GetVanancy(request.CompanyId);

                // Сериализуем список вакансий в JSON
                var responseJson = JsonConvert.SerializeObject(modelvac).ToArray();
                //modelvac = _vacancyService.GetVanancy(id);

               // var responseMessage = JsonConvert.SerializeObject(modelvac);
                //var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

                var properties = _rabbitMqChannel.CreateBasicProperties();
                properties.CorrelationId = ea.BasicProperties.CorrelationId;

                // _rabbitMqChannel.BasicPublish("", ea.BasicProperties.ReplyTo, properties, responseBytes);
                _rabbitMqChannel.BasicPublish("", ea.BasicProperties.ReplyTo, properties, Encoding.UTF8.GetBytes(responseJson));
            };

            _rabbitMqChannel.BasicConsume("vacancy_requests", true, consumer);
        }
        [HttpGet("GetAllVacancies")]
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
                        CompanyId = u.CompanyId,
                    };
                    model.Add(Vacancy);
                });
            }
            //_rabitMQ.SendVacanciesMessage(model);
            return model;
        }
        [HttpPost("MakeVacancy")]
        public async Task<ActionResult<VacancyViewModel>> MakeVacancy(VacancyViewModel model)
        {
            VanancyModel cv = new VanancyModel
            {
                NameJob = model.NameJob,
                TextJob = model.TextJob,
                CompanyId = model.CompanyId,
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
                vacancy.CompanyId = model.CompanyId;
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
                model.CompanyId = vacancy.CompanyId;
                return new ObjectResult(model);
            }
            return BadRequest();
        }
    }
}
