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
        public void Listen<TRequest>(Action<TRequest> on)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);
            var consumer = new EventingBasicConsumer(_rabbitMqChannel);
            consumer.Received += (model, ea) =>
            {
                var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());

                // Десериализуем запрос из JSON
                var request = JsonConvert.DeserializeObject<TRequest>(requestJson);
                on(request);
            };
            _rabbitMqChannel.BasicConsume("vacancy_requests", true, consumer);
        }
        public void SendVacanciesMessage<T>(VacancyViewModel response)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);
            int id = response.CompanyId;
            var res = _vacancyService.GetVanancy(id);
             var responseJson = JsonConvert.SerializeObject(res).ToArray();

             var properties = _rabbitMqChannel.CreateBasicProperties();

             _rabbitMqChannel.BasicPublish("", "vacancy_requests", properties, Encoding.UTF8.GetBytes(responseJson));
        }
    }
}
