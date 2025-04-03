using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using Application.DTOs;
using Application.Response;
using Core.DTOs;

namespace Application.Profiles
{
    public  class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Users, CreateUserDTO>().ReverseMap();
            CreateMap<Users, UserResponseDTO>().ReverseMap();
            CreateMap<Users, UpdateUserDto>().ReverseMap();


            CreateMap<UsersWithRole, Users>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();


            CreateMap<Countries, CountryDto>().ReverseMap();

            

        }

        
        
    }
}
