using Ramand_Domain.Models;

namespace Ramand_Application.ServiceContract.Persistence
{
    public interface IRabbitMqService
    {
        User GetUserById(int userId);
        void SendUser(User user);
    }
}