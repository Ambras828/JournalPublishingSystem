using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(string Email, string Password);
    }
}
