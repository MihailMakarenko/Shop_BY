using System.Security.Claims;

namespace UserService.Presentation.ActionFilters
{
    public interface IAccessControlService
    {
        Task<bool> CanAccessUserAsync(ClaimsPrincipal user, Guid targetUserId);
    }
}
