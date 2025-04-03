using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using Application.Contracts;
using DataAccess.Contracts;
using DataAccess.Models;
using Infrastructure.Contract;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Core.DTOs;
using System.Security.Authentication;

namespace Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IUserReadRepository userReadRepository;
        private readonly ITokenService _tokenService;
   
        private readonly IMapper _mapper;



        public AuthService(IUserWriteRepository userWriteRepository, ITokenService tokenService,  IMapper _mapper, IUserReadRepository userReadRepository)
        {
            this.userWriteRepository = userWriteRepository;
            _tokenService = tokenService;
            this.userReadRepository = userReadRepository;


            this._mapper = _mapper;
           
        }       
        public async Task<string> AuthenticateUser(string Email, string Password)
        {
             var user = await userReadRepository.GetUserByEmail(Email);
                var _passwordHasher = new PasswordHasher<Users>();

                if (user == null)
                {
                    throw new KeyNotFoundException("USer or emailID not found"); // User not found
                }
           



                // Verify password hash
                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                throw new AuthenticationException("Invalid credentials"); ; // Password mismatch
                }

                var userRoles = await userReadRepository.GetUserRoles(user.Id);

                var UserWithRole = new UsersWithRole
                {
                    Id = user.Id,  
                    Email=user.Email,
                    PasswordHash=user.PasswordHash,
                    Roles=userRoles


                };

                return _tokenService.GenerateToken(UserWithRole);
            
        }
    }
}
