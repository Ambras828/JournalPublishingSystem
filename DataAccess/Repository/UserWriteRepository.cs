using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Models;
using Infrastructure.Contract;
using DataAccess.QueryConstants;


namespace DataAccess.Repository
{
    public class UserWriteRepository:BaseRepository, IUserWriteRepository
    {

        public UserWriteRepository(IDbConnectionFatory _DbConnectionFatory)
            : base(_DbConnectionFatory)
        {
        }

        public async Task<int> CreateUser(Users user)
        {
            return await ExecuteScalarAsync<int>(UserQuery.CreateUser, user);
        }

        public async Task<int> UpdateUser(int id,Users user)
        {
            return await ExecuteAsync(UserQuery.UpdateUser,new { user, id = id });
        }

        public async Task<int> HardDeleteUser(int id)
        {
            return await ExecuteAsync(UserQuery.HardDeleteUser,new { id = id });
        }

        public async Task<int> SoftDeleteUser(Users user,int id)
        {
            return await ExecuteAsync(UserQuery.SoftDeleteUser, new { user, id = id });
        }

        public async Task<int> AssignRole(int UserId,int  RoleId)
        {
            return await ExecuteAsync(UserQuery.AssignRole, new { UserId, RoleId });
        }







    }
}
