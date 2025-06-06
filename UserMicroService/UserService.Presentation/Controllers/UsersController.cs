using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
using System.Security.Claims;
namespace UserService.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IServiceManager _serviceManager, IValidator<UserForUpdateDto> _updateValidator) : ControllerBase
    {
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id:guid}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (!await CanAccessUserAsync(User, id))
                return Forbid();
            
            var user = await _serviceManager.UserService.GetUserById(id, trackChanges: false);

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("collection", Name = "UserCollection")]
        public async Task<IActionResult> GetUsersCollections([FromQuery] IEnumerable<Guid> ids)
        {
            var users = await _serviceManager.UserService.GetUsersByIds(ids, trackChanges: false);

            return Ok(users);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!await CanAccessUserAsync(User, id))
                return Forbid();

            await _serviceManager.UserService.DeleteUser(id, trackChanges: false);

            return NoContent();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto userForUpdate)
        {
            _updateValidator.ValidateAndThrow(userForUpdate);

            if (!await CanAccessUserAsync(User, id))
                return Forbid();

            await _serviceManager.UserService.UpdateUser(id, userForUpdate, trackChanges: true);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}/deactivate")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            await _serviceManager.UserService.DeactivateUser(id, trackChanges: true);

            return NoContent();
        }


        private Task<bool> CanAccessUserAsync(ClaimsPrincipal user, Guid targetUserId)
        {
            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUser = user.IsInRole("User");

            if (isUser && targetUserId.ToString() != currentUserId)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
