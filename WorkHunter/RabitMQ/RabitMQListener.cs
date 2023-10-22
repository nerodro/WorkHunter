using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Vacancies.RabitMQ
{
    //public class RabitMQListener : IRabitMQListener
    //{
    //    public void TakeVacanciesMessage<T>(T message)
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
    //        var consumer = new EventingBasicConsumer(channel);
    //        consumer.Received += (model, eventArgs) =>
    //        {
    //            var body = eventArgs.Body.ToArray();
    //            var message = Encoding.UTF8.GetString(body);
    //        };
            
    //        channel.BasicConsume(queue: "vacancie", autoAck: true, consumer: consumer);
    //    }
    //}
}
