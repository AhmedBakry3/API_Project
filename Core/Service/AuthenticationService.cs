using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
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
                    Token = CreateTokenAsync(User)
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
                    Token = CreateTokenAsync(User)
                };
            }
            else
            {
                //Throw BadRequestException
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }
        private static string CreateTokenAsync(ApplicationUser User)
        {
            return "TOKEN _ TODO";
        }
    }
}
