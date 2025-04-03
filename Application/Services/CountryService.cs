using Application.Contracts;
using Application.DTOs;
using AutoMapper;
using DataAccess.Contracts;
using DataAccess.Models;
using DataAccess.Repository;
using Infrastructure.CustomeExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryService : ICountryService

    {
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly ICountryWriteRepository _countryWriteRepositoryClass;
        private readonly IMapper _mapper;
        public CountryService(ICountryReadRepository countryReadRepository, ICountryWriteRepository countryWriteRepositoryClass,IMapper mapper)
        {
            _countryReadRepository = countryReadRepository;
            _countryWriteRepositoryClass= countryWriteRepositoryClass;
            _mapper = mapper;

        }

        public  async Task<int> CreateCountry(CountryDto countryDto)
        {
            
            if(await _countryReadRepository.GetCountryByName(countryDto.CountryName)!=null)
            {
                throw new CountryAlreadyExistsException("Already Exist the country");
            }

            var user = _mapper.Map<Countries>(countryDto);
            
            return await _countryWriteRepositoryClass.CreateCountry(user);



        }
    }
}
