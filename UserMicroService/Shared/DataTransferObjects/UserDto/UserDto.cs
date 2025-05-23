using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.UserDto
{
    public record UserDto
    {
        public Guid Id { get; init; }

        public string? Email { get; init; }

        public string? Phone { get; init; }

        public string? FullName { get; init; }
    }
}
