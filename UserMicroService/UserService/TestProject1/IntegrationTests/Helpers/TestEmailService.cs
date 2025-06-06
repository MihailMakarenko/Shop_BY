using Contracts;
using Entites.Exceptions.EmailException;
using Entites.Exceptions.UsersException;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Tests.IntegrationTests.Helpers
{
    public class TestEmailService(IRepositoryManager _repositoryManager, IConfiguration _config) : IEmailService
    {
        public async Task ConfirmEmailByToken(string email, string token, bool trackChanges)
        {
            var user = await _repositoryManager.UserRepository.GetUserByEmail(email, trackChanges);

            if (user == null)
                throw new UserNotFoundByEmailException(email);

            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException();

            if (user.EmailConfirmToken != token)
                throw new InvalidEmailConfirmationTokenException();

            user.EmailConfirmed = true;
            await _repositoryManager.SaveAsync();
        }

        public async Task SendConfirmEmail(string email, string emailBodyUrl)
        {
            var subject = "Email confirmation";
            var emailBody = $"To confirm your email, please <a href=\"{emailBodyUrl}\">click here </a> ";
            await SendEmail(email, subject, emailBody);
        }

        public async Task SendResetPasswordEmail(string email, string emailBodyUrl)
        {
            var subject = "Password reset";
            var emailBody = $"To reset your password, please <a href=\"{emailBodyUrl}\">click here </a> ";
            await SendEmail(email, subject, emailBody);
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
