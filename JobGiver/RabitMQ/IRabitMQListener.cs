namespace Company.RabitMQ
{
    public interface IRabitMQListener
    {
        public Task TakeVacanciesMessage(string queue);
    }
}
