using FunctionAppArchitecture.Shared.Models;

namespace FunctionAppArchitecture.UserRegistration.Services
{
    public interface IValidationService
    {
        ActivityResponse Validate(User user);
    }
}
