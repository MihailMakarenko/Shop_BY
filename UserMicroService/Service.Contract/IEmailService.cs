using Shared.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IEmailService
    {
        Task SendConfirmEmail(string email, string emailBodyUrl);
        Task SendResetPasswordEmail(string email, string emailBodyUrl);
        Task ConfirmEmailByToken(string email, string token, bool trackChanges);
    }
}
