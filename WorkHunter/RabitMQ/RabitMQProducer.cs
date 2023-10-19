using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Vacancies.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendVacanciesMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };
            var connection = factory.CreateConnection();
            using
                var channel = connection.CreateChannel();
            channel.QueueDeclare("vacancie", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "vacancie", body: body);
        }
    }
}
