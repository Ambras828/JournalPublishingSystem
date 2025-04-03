using DataAccess.Contracts;
using DataAccess.Models;
using DataAccess.QueryConstants;
using Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CountryWriteRepository:BaseRepository,ICountryWriteRepository
    {

        public CountryWriteRepository(IDbConnectionFatory dbConnectionFatory):base(dbConnectionFatory) { }

        public async Task<int> CreateCountry(Countries country)
        {
            return await ExecuteScalarAsync<int>(UserQuery.CreateCountry,country);
            
        }

        public Task<int> DeleteCountry()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateCountry()
        {
            throw new NotImplementedException();
        }
    }
}
