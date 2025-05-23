using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.UserDto
{
    public class UserForAuthenticationDto
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
    }
}
