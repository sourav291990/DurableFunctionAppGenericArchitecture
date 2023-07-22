using FunctionAppArchitecture.Shared.Models;

namespace FunctionAppArchitecture.UserRegistration.Services
{
    public interface IUserService
    {
        ActivityResponse CreateUser(User user);
    }
}
