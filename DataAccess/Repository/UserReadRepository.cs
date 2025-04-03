using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Models;
using Infrastructure.Contract;
using DataAccess.QueryConstants;
using Core.DTOs;
using System.ComponentModel.DataAnnotations;
using Core.DTOs;

namespace DataAccess.Repository
{
    public class UserReadRepository : BaseRepository, IUserReadRepository
    {
     

        public UserReadRepository(IDbConnectionFatory DbConnectionFactory)
            : base(DbConnectionFactory)
        {
        }

        public async Task<IEnumerable<Users?>> GetUserByNameOrEmail(string Username, string Email)
        {
            return await SelectAsync<Users>(UserQuery.SelectUserByNameOrEmail, new { Username= Username, Email= Email });
         
        }

        public async Task<Users> GetUserByEmail( string Email)
        {
            return await SelectFirstOrDefaultAsync<Users>(UserQuery.SelectUserByEmail, new { Email });

        }

        public async Task<List<string>> GetUserRoles(int UserId)
        {
            return await SelectUserRolesAsync<string>(UserQuery.SelectRolebyId, UserId);
        }

        public async  Task<(IEnumerable<Users>, int totalRecords)> GetUsers(UserQueryParameters userQueryParameters)
        {
            string SelectUserBase = UserQuery.SelectUserBase;
            string SelectUserCount = UserQuery.SelectUserCount;
            
            if (!string.IsNullOrEmpty(userQueryParameters.Search))
            {
                SelectUserBase += " AND (UserName LIKE @Search or Email LIKE @Search)";
                SelectUserCount += " AND (UserName LIKE @Search or Email LIKE @Search)";

            }
            if(userQueryParameters.IsActive.HasValue) 
                {
                SelectUserBase += " AND IsActive=@IsActive";
                SelectUserCount += " AND  IsActive=@IsActive";


            }
            // Sorting
            SelectUserBase += $" ORDER BY {userQueryParameters.SortBy} {userQueryParameters.SortOrder.ToUpper()}";

            // Pagination
            SelectUserBase += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new
            {
                Offset = (userQueryParameters.PageNumber - 1) * userQueryParameters.PageSize,
                Pagesize = userQueryParameters.PageSize,
                Search = $"%{userQueryParameters.Search}%",
                IsActive=userQueryParameters.IsActive

            };
           var Combinedquery = $"{SelectUserBase}; {SelectUserCount}";


         return  await SelectMultipleAsync<Users>(Combinedquery, parameters);
         
        }


        public async Task<Users> GetUserById(int id)
        {
            var user = await SelectFirstOrDefaultAsync<Users>(UserQuery.SelectUserById, new { id });
            return user;

        }

    }
}
