using ChatApp.Core.DataTransferObjects;
using ChatApp.Core.DataTransferObjects.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegistrationRequest);
        Task<bool> ValidateUser(UserLoginDto userAuthRequest);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
