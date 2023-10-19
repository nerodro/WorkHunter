namespace Vacancies.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendVacanciesMessage <T> (T message);
    }
}
