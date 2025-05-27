using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.UserDto;

namespace Service.Contract
{
    public interface IAutenticationService
    {
        Task<IdentityResult> RegisterUser(UserForCreationDto userForCreation, string emailToken);
        Task<bool> LoginUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<string> ForgotPassword(string email);
        Task ResetPassword(ResetPasswordDto model);

    }
}
