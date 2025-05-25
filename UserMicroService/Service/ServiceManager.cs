using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAutenticationService> _authenticationService;
        private readonly Lazy<IEmailService> _emailService;
        public ServiceManager(IConfiguration config, IRepositoryManager repositoryManager, IMapper mapper,
             UserManager<User> userManager, IOptions<JwtConfiguration> configuration, RoleManager<IdentityRole> roleManager)
        {
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAutenticationService>(() => new AuthenticationService(userManager, mapper, configuration, roleManager, repositoryManager));
            _emailService = new Lazy<IEmailService>(() => new EmailService(repositoryManager,config));
        }

        public IUserService UserService => _userService.Value;

        public IAutenticationService AutenticationService => _authenticationService.Value;

        public IEmailService EmailService => _emailService.Value;
    }
}
