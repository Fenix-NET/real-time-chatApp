using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces
{
    public interface ITokenService // Этот сервис надо переработать, вместе с AuthService.
    {
        ClaimsPrincipal ValidateToken(string token);
    }
}
