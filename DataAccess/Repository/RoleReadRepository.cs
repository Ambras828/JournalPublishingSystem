using DataAccess.Contracts;
using Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
    using DataAccess.QueryConstants;

namespace DataAccess.Repository
{
    public class RoleReadRepository:BaseRepository,IRoleReadRepository
    {
        public RoleReadRepository(IDbConnectionFatory dbConnectionFatory) : base(dbConnectionFatory)
        {
        }

        public async Task<Roles> GetRoleIdByName(string RoleName)
        {
            return await SelectFirstOrDefaultAsync<Roles>(UserQuery.SelectRoleIdbyName, new { RoleName });
        }
    }
}
