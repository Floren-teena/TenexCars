﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TenexCars.DTOs;
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

        public async Task SendOperatorSetPasswordEmailAsync(EmailDto emailDto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tenex Cars", _config["EmailSettings:Username"]));
                message.To.Add(new MailboxAddress("", emailDto.To)); // Ensure 'emailDto.To' is valid
                message.Subject = emailDto.Subject;
                message.Body = new TextPart("html")
                {
                    Text = emailDto.Body // Ensure 'emailDto.Body' is valid HTML
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Remove this for production
                    await client.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP Command Exception: {ex.Message}, Status Code: {ex.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }

        public async Task ContactApplicantEmailAsync(EmailDto emailDto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tenex Cars", _config["EmailSettings:Username"]));
                message.To.Add(new MailboxAddress("", emailDto.To));
                message.Subject = emailDto.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = emailDto.Body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), false);
                    await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }

        public async Task ApproveSubscriptionEmail(EmailDto emailDto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tenex Cars", _config["EmailSettings:Username"]));
                message.To.Add(new MailboxAddress("", emailDto.To));
                message.Subject = emailDto.Subject;
                message.Body = new TextPart("html")
                {
                    Text = emailDto.Body
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]), false);
                    await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }
    }
}
