using Application.DTOs;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICountryService
    {

        Task<int> CreateCountry(CountryDto countryDto);

    }
}
