using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface ICountryWriteRepository
    {
        Task<int> CreateCountry(Countries country);
        Task<int> UpdateCountry();
        Task<int> DeleteCountry();
            

    }
}
