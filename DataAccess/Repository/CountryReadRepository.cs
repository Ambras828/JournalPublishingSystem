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
    public class CountryReadRepository:BaseRepository,ICountryReadRepository
    {
        public CountryReadRepository(IDbConnectionFatory dbConnection):base(dbConnection) { }


        public async Task<Countries> GetCountryByName(string CountryName)
        {

            return await SelectFirstOrDefaultAsync<Countries>(UserQuery.SelectCountryByName, new { CountryName = CountryName });
        }
    }
}
