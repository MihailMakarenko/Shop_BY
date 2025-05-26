using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
namespace UserService.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    
    public class UsersController(IServiceManager _serviceManager,  IValidator<UserForUpdateDto> _updateValidator) : ControllerBase
    {
        [HttpGet("{id:guid}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            Console.WriteLine("rt");
            var user = await _serviceManager.UserService.GetUserById(id, trackChanges: false);

            return Ok(user);
        }

        [HttpGet("collection/({ids})", Name = "UserCollection")]
        public async Task<IActionResult> GetUsersCollections(IEnumerable<Guid> ids)
        {
            var users = await _serviceManager.UserService.GetUsersByIds(ids, trackChanges: false);

            return Ok(users);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _serviceManager.UserService.DeleteUser(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto userForUpdate)
        {
            _updateValidator.ValidateAndThrow(userForUpdate);

            await _serviceManager.UserService.UpdateUser(id, userForUpdate, trackChanges: true);

            return NoContent();
        }

    }
}
