using Ramand_Application.Model;

using Ramand_Domain.Models;

namespace Ramand_Application.ServiceContract.Persistence
{
    public interface IAuthenticationService
    {
        AuthResponse Login(AuthRequest request);
        List<User> GetUsers();
    }
}