using Application.Contracts;
using DataAccess.Contracts;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs;
using Core.DTOs;
using Application.Response;
using Microsoft.AspNetCore.Identity;
namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _uSerReadrepository;
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserService(IUserReadRepository uSerReadrepository, IMapper mapper, IUserWriteRepository userWriteRepository, IRoleService roleService)
        {
            _uSerReadrepository = uSerReadrepository;
            _mapper = mapper;
            this.userWriteRepository = userWriteRepository;
            _roleService = roleService;
        }

        public async  Task<PaginatedResult<UserResponseDTO>> GetAllUsersAsync(UserQueryParameters userQueryParameters)
        {

            userQueryParameters.SortBy= validatedSortBy(userQueryParameters.SortBy);
            userQueryParameters.SortOrder = userQueryParameters.SortOrder.ToLower() == "desc" ? "DESC" : "ASC";
            var (users,totalRecords)= await _uSerReadrepository.GetUsers(userQueryParameters);
            var userdto = _mapper.Map < IEnumerable<UserResponseDTO>>(users );
            return new PaginatedResult<UserResponseDTO>
            {
                Data = userdto,
                TotalRecords = totalRecords,
                PageNumber = userQueryParameters.PageNumber,
                PageSize = userQueryParameters.PageSize,
                
                


            };
        }
        private string validatedSortBy(string sortBy)
        {
            var allowedColumns = new [] { "Id", "Username", "Email", "CreatedDate" };
            return allowedColumns.Contains(sortBy) ? sortBy : "Id";
        }
        public async Task<int> CreateUser(CreateUserDTO createUserDTO)
        {
            var existingUser = await _uSerReadrepository.GetUserByNameOrEmail(createUserDTO.Username, createUserDTO.email);
            if (existingUser.Count()>0)
            {
                throw new Exception("User with same email or username already registered");
            }

            //Use Microsoft's Identity PasswordHasher for better security
            var passwordHasher = new PasswordHasher<Users>();


            // Map DTO to User entity
            var user = _mapper.Map<Users>(createUserDTO);
            user.PasswordHash = passwordHasher.HashPassword(user, createUserDTO.Password);
            user.IsActive = true;
            user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;

            int UserID= await userWriteRepository.CreateUser  (user);

            var UserRole = await _roleService.GetRoleIdByName("Viewer");
            if (UserRole.id != 0)
            {
                await userWriteRepository.AssignRole(UserID, UserRole.id);
            }
            return UserID;

        }

        public async Task<CreateUserDTO> GetUserById(int id)
        {
            var user=await _uSerReadrepository.GetUserById(id);
            return _mapper.Map<CreateUserDTO>(user);
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUserByNameOrEmail(string Username, string Email)
        {
            var user=await _uSerReadrepository.GetUserByNameOrEmail(Username, Email);
            return _mapper.Map<IEnumerable<UserResponseDTO>>(user);
        }

      public async  Task<Users> GetUserByEmail(string Email)
        {
            return await _uSerReadrepository.GetUserByEmail(Email);
        }

        public async Task<int> UpdateUser(int id, UpdateUserDto user)
        {
            var users = _mapper.Map<Users>(user);
            users.UpdatedDate= DateTime.UtcNow;  
            return await userWriteRepository.UpdateUser(id, users);
        }

        public async Task<int> HardDeleteUser(int id)
        {
            var user = await _uSerReadrepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with {id} not found");


            }
            return await userWriteRepository.HardDeleteUser(id);
        }

        public async Task<int> SoftDeleteUser(int id)
        {
            var user = await _uSerReadrepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with {id} not found");


            }
            user.UpdatedDate = DateTime.UtcNow;
            user.IsDeleted= true;

            return await userWriteRepository.SoftDeleteUser(user,id);


        }
    }
}
