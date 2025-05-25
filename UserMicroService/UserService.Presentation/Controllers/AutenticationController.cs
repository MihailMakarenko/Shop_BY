using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IServiceManager _serviceManager, IMapper _mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto UserForRegistration)
        {
           
            string emailToken = Guid.NewGuid().ToString();
            var result = await _serviceManager.AutenticationService.RegisterUser(UserForRegistration, emailToken);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            var emailBodyUrl = Request.Scheme + "://" + Request.Host + Url.Action("confirmemail", "authentication", new { email = UserForRegistration.Email, token = emailToken });
            await _serviceManager.EmailService.SendConfirmEmail(UserForRegistration.Email!, emailBodyUrl);
            return Ok("Registration was successful. Check email.");
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([EmailAddress] string email, string token)
        {
            await _serviceManager.EmailService.ConfirmEmailByToken(email, token, trackChanges: true);
            return Ok("Confirmation was successful.");
        }

    }
}
