using Vacancies.Models;

namespace Vacancies.RabitMQ
{
    public interface IRabitMQProducer
    {
       // public void Listen<TRequest>(Action<VacancyViewModel> on);
        void SendVacanciesMessage<T>(VacancyViewModel response);
    }
}
