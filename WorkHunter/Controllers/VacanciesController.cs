using DomainLayer.Models.Vacancies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryLayer.DataBasesContext;
using ServiceLayer.Property.VacanciesService;
using System.Text;
using System.Threading.Channels;
using System.Transactions;
using Vacancies.Models;
using Vacancies.RabitMQ;

namespace WorkHunter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IConnection _rabbitMqConnection;
        private readonly IModel _rabbitMqChannel;
        private readonly IRabitMQProducer _rabbitMqProducer;
        public VacanciesController(IVacancyService vacancyService, IRabitMQProducer rabitMQProducer)
        {
            _vacancyService = vacancyService;
            _rabbitMqConnection = GetRabbitMqConnection();
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqProducer = rabitMQProducer;
            var vac = new RabitMQProducer(vacancyService, _rabbitMqConnection, _rabbitMqChannel);
            vac.SendVacanciesMessage();
            //InitializeRabbitMqService();
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
        public async Task<ActionResult<VacancyViewModel>> SingleVacancy(int id)
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
        //private void InitializeRabbitMqService()
        //{
        //    _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);

        //    var consumer = new EventingBasicConsumer(_rabbitMqChannel);


        //    VanancyModel modelvac2 = _vacancyService.GetVanancy(2);
        //    consumer.Received += (model, ea) =>
        //    {
        //        var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());

        //        // Десериализуем запрос из JSON
        //        var request = JsonConvert.DeserializeObject<VacancyViewModel>(requestJson);

        //        int id = request.CompanyId;

        //        using (var scope = new TransactionScope())
        //        {
        //            var modelvac2 = _vacancyService.GetVacanciesForCompany(id).ToList();

        //            var responseJson = JsonConvert.SerializeObject(modelvac2).ToArray();

        //            var properties = _rabbitMqChannel.CreateBasicProperties();
        //            properties.CorrelationId = ea.BasicProperties.CorrelationId;

        //            _rabbitMqChannel.BasicPublish("", ea.BasicProperties.ReplyTo, properties, Encoding.UTF8.GetBytes(responseJson));

        //            scope.Complete();
        //        }
        //    };
        //    _rabbitMqChannel.BasicConsume("vacancy_requests", true, consumer);
        //}
        private IConnection GetRabbitMqConnection()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            return factory.CreateConnection();
        }
    }
}
