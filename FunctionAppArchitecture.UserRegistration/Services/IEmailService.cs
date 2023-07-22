using FunctionAppArchitecture.Shared.Models;

namespace FunctionAppArchitecture.UserRegistration.Services
{
    public interface IEmailService
    {
        ActivityResponse SendEmail(Email email);
    }
}
