using AutoMapper;
using Contracts;
using Entites.Exceptions;
using Entites.Exceptions.UsersException;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public class AuthenticationService : IAutenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfiguration _jwtConfiguration;

        private User? _user;

        public AuthenticationService(UserManager<User> userManager,
           IMapper mapper, IOptions<JwtConfiguration> configuration,
           RoleManager<IdentityRole> roleManager, IRepositoryManager repositoryManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;
            Console.WriteLine(_configuration.Value);
            _jwtConfiguration = _configuration.Value;
            _repositoryManager = repositoryManager;
        }


        public async Task<IdentityResult> RegisterUser(UserForCreationDto userForRegistrartion, string emailToken)
        {
            var user = _mapper.Map<User>(userForRegistrartion);
            user.EmailConfirmToken = emailToken;
            // Checking the existence of a phone


            var result = await _userManager.CreateAsync(user, userForRegistrartion.Password!);


            if (result.Succeeded)
                await AddToRolesIfExist(user, userForRegistrartion);

            return result;
        }

        public async Task<bool> LoginUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email!);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password!));

            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user!.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);
            if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();
            _user = user;
            return await CreateToken(populateExp: false);
        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new UserNotFoundByEmailException(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user!);
            await _userManager.UpdateAsync(user!);

            return token;
        }

        public async Task ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
                throw new UserNotFoundByEmailException(model.Email!);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user!, model.ResetToken!, model.NewPassword!);

            if (!resetPasswordResult.Succeeded)
                throw new PasswordResetException();
        }

        private SigningCredentials GetSigningCredentials()
        {

            var key = _jwtConfiguration.SecretKey
                ?? throw new InvalidOperationException("JWT SecretKey not configured");

            var keyBytes = Encoding.UTF8.GetBytes(key);

            var secret = new SymmetricSecurityKey(keyBytes);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.Email!),
                new Claim(ClaimTypes.NameIdentifier, _user.Id)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpiresInMinutes), // Убрали Convert
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        private async Task AddToRolesIfExist(User user, UserForCreationDto userForReqistration)
        {
            bool allRolesExist = true;

            if (userForReqistration.Roles is not null &&
                userForReqistration.Roles.Count != 0)
            {
                foreach (string role in userForReqistration.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(role) == false)
                    {
                        allRolesExist = false;
                        break;
                    }
                }

                if (allRolesExist)
                {
                    await _userManager.AddToRolesAsync(user, userForReqistration.Roles);
                }
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);

            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey!)),
                ValidateLifetime = true,

                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {

                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
