using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        public Task<bool> CheckEmailAsync(string Email);
        public Task<AddressDto> GetCurrentUserAddressAsync(string Email);
        public Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string Email);
        public Task<UserDto> GetCurrentUser(string Email);
    }
}
