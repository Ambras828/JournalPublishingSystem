using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IUserWriteRepository
    {
        Task<int> CreateUser(Users user);
        Task<int> UpdateUser(int Id, Users user);
        Task<int> HardDeleteUser(int id);
        Task<int> SoftDeleteUser(Users user, int id);
        Task<int> AssignRole(int UserId, int RoleId);

    }
}
