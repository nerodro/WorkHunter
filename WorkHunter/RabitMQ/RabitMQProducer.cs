using DomainLayer.Models.Vacancies;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLayer.Property.VacanciesService;
using ServiceLayer.Property.WorkerService;
using System.Text;
using System.Transactions;
using Vacancies.Models;

namespace Vacancies.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly IVacancyService _vacancyService;
        private readonly IConnection _rabbitMqConnection;
        private IModel _rabbitMqChannel;
        public RabitMQProducer(IVacancyService vacancyService, IConnection rabbitMqConnection, IModel rabbitMqChannel)
        {
            _vacancyService = vacancyService;
            _rabbitMqConnection = rabbitMqConnection;
            _rabbitMqChannel = rabbitMqChannel;
        }

        public void SendVacanciesMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);

            var consumer = new EventingBasicConsumer(_rabbitMqChannel);


            VanancyModel modelvac2 = _vacancyService.GetVanancy(2);
            consumer.Received += (model, ea) =>
            {
                var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Десериализуем запрос из JSON
                var request = JsonConvert.DeserializeObject<VacancyViewModel>(requestJson);

                int id = request.CompanyId;

                using (var scope = new TransactionScope())
                {
                    var modelvac2 = _vacancyService.GetVacanciesForCompany(id).ToList();

                    var responseJson = JsonConvert.SerializeObject(modelvac2).ToArray();

                    var properties = _rabbitMqChannel.CreateBasicProperties();
                    properties.CorrelationId = ea.BasicProperties.CorrelationId;

                    _rabbitMqChannel.BasicPublish("", ea.BasicProperties.ReplyTo, properties, Encoding.UTF8.GetBytes(responseJson));

                    scope.Complete();
                }
            };
            _rabbitMqChannel.BasicConsume("vacancy_requests", true, consumer);
        }
    }
}
