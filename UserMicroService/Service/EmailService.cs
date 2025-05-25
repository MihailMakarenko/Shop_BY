using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;

namespace Service
{
    public class EmailService(IRepositoryManager _repositoryManager ,IConfiguration _config) : IEmailService
    {
        public async Task ConfirmEmailByToken(string email, string token, bool trackChanges)
        {
            var user = await _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges);

            if (user.EmailConfirmed)
                throw new Exception("Email is already confirmed.");

            if (user.EmailConfirmToken != token)
                throw new Exception("Invalid token.");

            user.EmailConfirmed = true;
            await _repositoryManager.SaveAsync();
        }

        public async Task SendConfirmEmail(string email, string emailBodyUrl)
        {
            var subject = "Email confirmation";
            var emailBody = $"To confirm your email <a href=\"{emailBodyUrl}\">click here </a> ";
            await SendEmail(email, subject, emailBody);
        }

        public Task SendResetPasswordEmail(string email, string emailBodyUrl)
        {
            throw new NotImplementedException();
        }


        private async Task SendEmail(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_config["Email:From:Name"], _config["Email:From:Address"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_config["Email:SmtpServer"], Convert.ToInt32(_config["Email:Port"]), Convert.ToBoolean(_config["Email:UseSSL"]));
                await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
                var res1 = await client.SendAsync(emailMessage);
            }
            catch
            {
                throw new Exception("Email sender trouble");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

    }
}
