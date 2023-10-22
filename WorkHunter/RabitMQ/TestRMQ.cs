using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Vacancies.Models;
using ServiceLayer.Property.VacanciesService;

namespace Vacancies.RabitMQ
{
    //public class TestRMQ 
    //{
    //    private readonly IVacancyService _vacancyService;
    //    public TestRMQ(IVacancyService vacancyService)
    //    {
    //        _vacancyService = vacancyService;
    //    }
    //    public void StartListen()
    //    {
    //        var connectionFactory = new ConnectionFactory
    //        {
    //            HostName = "localhost",
    //            Port = 5672,
    //            UserName = "guest",
    //            Password = "guest",
    //        };
    //        var connection = connectionFactory.CreateConnection();
    //        using
    //            var channel = connection.CreateModel();
    //        channel.QueueDeclare("vacancie", exclusive: false);
    //        var consumer = new EventingBasicConsumer(channel);
    //        consumer.Received += (model, ea) =>
    //        {
    //            var requestJson = Encoding.UTF8.GetString(ea.Body.ToArray());
    //            var request = JsonConvert.DeserializeObject<VacancyViewModel>(requestJson);
    //            int id = (int)request.Id;
    //            var vacancies = _vacancyService.GetVanancy(id);
    //            var responseJson = JsonConvert.SerializeObject(vacancies);
    //            var list = Encoding.UTF8.GetBytes(responseJson);
    //            channel.BasicPublish(exchange: "", routingKey: "vacancie", body: list);
    //        };
    //        channel.BasicConsume(queue: "vacancie", autoAck: true, consumer: consumer);
    //    }
    //}
}
