using TenexCars.DTOs;

namespace TenexCars.Interfaces
{
    public interface IEmailService
    {
        Task SendOperatorSetPasswordEmailAsync(EmailDto emailDto);
    }
}
