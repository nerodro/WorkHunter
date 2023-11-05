using DomainLayer.Models.Vacancies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CompanyRepository.DataBasesContext;
using ServiceLayer.Property.VacanciesService;
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
        
        public void SendVacanciesMessage<T>(VacancyViewModel response)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _rabbitMqChannel = _rabbitMqConnection.CreateModel();
            _rabbitMqChannel.QueueDeclare("vacancy_requests", false, false, false, null);
            int id = response.CompanyId;
            var optionsBuilder = new DbContextOptionsBuilder<VacancyContext>();
            VanancyModel res = new VanancyModel();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Vacancies;Username=postgres;Password=nerodro26;");
            using (var dbContext = new VacancyContext(optionsBuilder.Options))
            {
                res = dbContext.Vanancy.FirstOrDefault(x => x.Id == id);
                dbContext.Dispose();
            }
            var responseJson = JsonConvert.SerializeObject(res).ToArray();

             var properties = _rabbitMqChannel.CreateBasicProperties();

             _rabbitMqChannel.BasicPublish("", "company_vacancies_response_queue", properties, Encoding.UTF8.GetBytes(responseJson));
        }
        public void ListenAnsw()
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Создаем очередь ответов
                channel.QueueDeclare("vacancy_requests", false, false, false, null);
                channel.QueueDeclare("company_vacancies_response_queue", false, false, false, null);

                // Создаем обработчик для сообщений из очереди ответов
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    // Получаем сообщение из очереди и десериализуем его в ответ
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var request = JsonConvert.DeserializeObject<VacancyViewModel>(message);
                    SendVacanciesMessage<VacancyViewModel>(request);
                };
                _rabbitMqChannel.BasicConsume("vacancy_requests", true, consumer);
            };
        }
    }
}
