using Vacancies.Models;

namespace Vacancies.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void Listen<TRequest>(Action<TRequest> on);
        void SendVacanciesMessage<T>(VacancyViewModel response);
    }
}
