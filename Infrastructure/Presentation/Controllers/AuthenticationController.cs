using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Login
        [HttpPost("Login")] //POST : BaseUrl/api/authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }

        //Register
        [HttpPost("Register")] //POST : BaseUrl/api/authentication/Register 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }

        //Check Email
        [HttpGet("CheckEmail")] // GET : BaseUrl/api/authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }
        //GetCurrentUser
        [Authorize]
        [HttpGet("GetCurrentUser")] // GET : BaseUrl/api/authentication/GetCurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUser(GetEmailFromToken());
            return Ok(AppUser);
        }
        //GetCurrentUserAddress
        [Authorize]
        [HttpGet("Address")] // GET : BaseUrl/api/authentication/Address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken());
            return Ok(Address);
        }
        //UpdateCurrentUserAddress
        [Authorize]
        [HttpPut("Address")] // PUT : BaseUrl/api/authentication/Address
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var Address = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(addressDto, GetEmailFromToken());
            return Ok(Address);
        }
    }
}
