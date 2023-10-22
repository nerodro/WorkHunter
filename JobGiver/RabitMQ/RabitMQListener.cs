using JobGiver.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Vacancies.Models;

namespace Company.RabitMQ
{
    public class RabitMQListener : IRabitMQListener
    {
        public async Task TakeVacanciesMessage(string queue)
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
                var channel = connection.CreateModel();
            //var channel = connection.CreateChannel();
            channel.QueueDeclare(queue, exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
            //return Task.CompletedTask;
        }
    }
}
