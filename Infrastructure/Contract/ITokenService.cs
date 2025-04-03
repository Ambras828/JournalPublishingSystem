using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;


namespace Infrastructure.Contract
{
    public interface ITokenService
    {
        public string GenerateToken(UsersWithRole usersWithRole);
    }
}
