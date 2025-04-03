using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Core.DTOs;

namespace DataAccess.Contracts
{
    public  interface IUserReadRepository
    {

        Task<(IEnumerable<Users>, int totalRecords)> GetUsers(UserQueryParameters userQueryParameters);
        Task<Users> GetUserById(int id);
        Task<IEnumerable<Users?>> GetUserByNameOrEmail(string Username, string Email);
        Task<Users> GetUserByEmail(string Email);
        Task<List<string>> GetUserRoles(int UserId);
    }
}
