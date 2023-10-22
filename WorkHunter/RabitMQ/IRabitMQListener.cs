namespace Vacancies.RabitMQ
{
    public interface IRabitMQListener
    {
        public void TakeVacanciesMessage<T>(T message);
    }
}
