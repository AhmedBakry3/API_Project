using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationService(UserManager<ApplicationUser> _userManager , IConfiguration _configuration , IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserDto> GetCurrentUser(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto
            {
                Email = User.Email!,
                DisplayName = User.DisplayName,
                Token = await CreateTokenAsync(User)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include(E => E.Address).FirstOrDefaultAsync(E => E.Email == Email)
                                                       ?? throw new UserNotFoundException(Email);

            return _mapper.Map<Address, AddressDto>(User.Address);

        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string Email)
        {
            var User = await _userManager.Users.Include(E => E.Address).FirstOrDefaultAsync(E => E.Email == Email)
                                                       ?? throw new UserNotFoundException(Email);
            if(User.Address is not null) //Update
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.Street = addressDto.Street;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
            }
            else //Add new Address
            {
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<Address, AddressDto>(User.Address);
        }


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
