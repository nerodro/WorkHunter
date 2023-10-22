using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceLayer.Property.VacanciesService;
using ServiceLayer.Property.WorkerService;
using System.Text;
using Vacancies.Models;

namespace Vacancies.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly IVacancyService _vacancyService;
        public RabitMQProducer(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }
        public void SendVacanciesMessage<T>(T message)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Создаем очередь сообщений
                channel.QueueDeclare("vacancy_requests", false, false, false, null);

                // Создаем очередь для ответов

                // Создаем консумера для чтения сообщений из очереди
                var consumer = new EventingBasicConsumer(channel);

                // Устанавливаем обработчик сообщений
                consumer.Received += (model, ea) =>
                {
                    var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());

                    // Десериализуем запрос из JSON
                    var request = JsonConvert.DeserializeObject<VacancyViewModel>(requestJson);

                    // Выполняем запрос в базе данных для получения списка вакансий для компании
                    var vacancies = _vacancyService.GetVanancy(request.CompanyId);

                    // Сериализуем список вакансий в JSON
                    var responseJson = JsonConvert.SerializeObject(vacancies);

                    // Отправляем ответ в очередь ответов
                    channel.BasicPublish("", "vacancy_requests", null, Encoding.UTF8.GetBytes(responseJson));
                };

                // Стартуем прослушивание очереди
                channel.BasicConsume("vacancy_requests", true, consumer);
            }
        }
        //}
        //public class RabitMQProducer : IRabitMQProducer
        //{
        //    public void SendVacanciesMessage<T>(T message)
        //    {
        //        var factory = new ConnectionFactory
        //        {
        //            HostName = "localhost",
        //            Port = 5672,
        //            UserName = "guest",
        //            Password = "guest",
        //        };
        //        var connection = factory.CreateConnection();
        //        using
        //            var channel = connection.CreateChannel();
        //        channel.QueueDeclare("vacancie", exclusive: false);
        //        var json = JsonConvert.SerializeObject(message);
        //        var body = Encoding.UTF8.GetBytes(json);
        //        channel.BasicPublish(exchange: "", routingKey: "vacancie", body: body);
        //    }
    }
}
