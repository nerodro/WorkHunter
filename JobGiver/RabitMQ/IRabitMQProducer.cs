namespace Company.RabitMQ
{
    public interface IRabitMQProducer
    {
        public Task SendVacanciesMessage <T> (T message);
    }
}
