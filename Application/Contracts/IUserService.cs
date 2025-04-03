using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.DTOs;
using DataAccess.Models;
using Application.Response;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<PaginatedResult<UserResponseDTO>> GetAllUsersAsync(UserQueryParameters userQueryParameters);
        Task<int> CreateUser(CreateUserDTO createUserDTO);
        Task<CreateUserDTO> GetUserById(int id);
        Task<int> UpdateUser(int id, UpdateUserDto Uuser);
        Task<int> HardDeleteUser(int id);
        Task<int> SoftDeleteUser(int id);
        Task<IEnumerable<UserResponseDTO>> GetUserByNameOrEmail(string Username, string Email);
        Task<Users> GetUserByEmail(string Email);
    }
}
