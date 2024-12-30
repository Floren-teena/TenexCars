using TenexCars.DTOs;

namespace TenexCars.Interfaces
{
    public interface IEmailService
    {
        Task SendOperatorSetPasswordEmailAsync(EmailDto emailDto);
        Task ContactApplicantEmailAsync(EmailDto emailDto);
        Task ApproveSubscriptionEmail(EmailDto emailDto);
    }
}
