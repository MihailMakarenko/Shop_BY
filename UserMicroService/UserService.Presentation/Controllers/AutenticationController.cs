using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
using System.ComponentModel.DataAnnotations;

namespace UserService.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IServiceManager _serviceManager, IValidator<UserForCreationDto> _postValidator, IValidator<UserForAuthenticationDto> _authValidator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto UserForRegistration)
        {
            _postValidator.ValidateAndThrow(UserForRegistration);

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

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto authUser)
        {
            _authValidator.ValidateAndThrow(authUser);

            if (!await _serviceManager.AutenticationService.LoginUser(authUser))
                return Unauthorized();

            var tokenDto = await _serviceManager.AutenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await
            _serviceManager.AutenticationService.RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([EmailAddress] string email, string token)
        {
            await _serviceManager.EmailService.ConfirmEmailByToken(email, token, trackChanges: true);
            return Ok("Confirmation was successful.");
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([EmailAddress] string email)
        {
            var token = await _serviceManager.AutenticationService.ForgotPassword(email);
            var emailBodyUrl = Request.Scheme + "://" + Request.Host + Url.Action("resetpassword", "authentication", new { email, token });
            await _serviceManager.EmailService.SendResetPasswordEmail(email, emailBodyUrl);
            return Ok($"Check {email} email. You may now reset your password within 1 hour.");
        }

        [HttpGet("resetpassword")]
        public ActionResult<ResetPasswordDto> ResetPassword([EmailAddress] string email, string token)
        {
            var model = new ResetPasswordDto
            {
                Email = email,
                NewPassword = "",
                ResetToken = token
            };
            return Ok(model);
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            await _serviceManager.AutenticationService.ResetPassword(model);
            return Ok("Password has been successfully changed!");
        }
    }
}
