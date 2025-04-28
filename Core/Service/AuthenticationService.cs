using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager , IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Check Email if Exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            //Check Password 
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordValid)
                return new UserDto
                {
                    //Return UserDto
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)
                };
            else
                //Throw UnauthorizedException
                throw new UnauthorizedException();
        }
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //mapping from RegisterDto to ApplicationUser
            var User = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };
            //Create User [Application User]
            var result = await _userManager.CreateAsync(User, registerDto.Password);
            if (result.Succeeded)
            {
                //Return UserDto
                return new UserDto
                {
                    //Return UserDto
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                //Throw BadRequestException
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }
        private  async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, User.Email!),
                new Claim(ClaimTypes.NameIdentifier, User.Id),
                new Claim(ClaimTypes.Name, User.UserName!)
            };
            var Roles = await _userManager.GetRolesAsync(User);
            foreach(var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
