using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IEmailService EmailService { get; }
        IAutenticationService AutenticationService { get; }
    }
}
