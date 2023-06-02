using DonaMenina.Entities;

namespace DonaMenina.Helper
{
    public interface IUserSession
    {
        void BuildUserSession(Worker worker);
        void EndUserSession();
        Worker GetWorkerSession();

    }
}
