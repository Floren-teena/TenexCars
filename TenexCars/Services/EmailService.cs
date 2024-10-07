using TenexCars.Interfaces;

namespace TenexCars.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }


    }
}
