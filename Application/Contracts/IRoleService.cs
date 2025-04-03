using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IRoleService
    {
        Task<Roles> GetRoleIdByName(string RoleName);
    }
}
