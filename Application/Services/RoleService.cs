using Application.Contracts;
using DataAccess.Contracts;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService:IRoleService
    {
        private readonly IRoleReadRepository roleReadRepository;
            
        public RoleService(IRoleReadRepository roleReadRepository) 
            {
                  this.roleReadRepository = roleReadRepository;
            }

        public async Task<Roles> GetRoleIdByName(string RoleName)
        {
            return await roleReadRepository.GetRoleIdByName(RoleName);
        }
    }
}
