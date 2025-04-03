using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using Infrastructure.Constants;
using DataAccess.Models;
using System.Data.SqlTypes;
using DataAccess.QueryConstants;

namespace DataAccess.Repository
{
    public class BaseRepository
    {
        private readonly IDbConnectionFatory dbConnectionFatory;
        private readonly int _commandTimeout = 30;

        public BaseRepository(IDbConnectionFatory dbConnectionFatory, IOptions<sqlCommandTimeout> sqlCommandTimeout = null)
        {
            this.dbConnectionFatory = dbConnectionFatory;

            if (sqlCommandTimeout != null)
            {
                _commandTimeout = sqlCommandTimeout.Value.sqlCommandTimeoutInSeconds;
            }
        }
        public async Task<(IEnumerable<T>, int totalRecords)> SelectMultipleAsync<T>(string sqlString, object parameters)
        {
            using var multi = await dbConnectionFatory.Connection.QueryMultipleAsync(sqlString, parameters, commandTimeout: _commandTimeout);

            // Reading data
            var users = multi.Read<T>();
            var totalRecords = multi.ReadSingle<int>();

            // Return as a DTO
            return (users, totalRecords);
        }
        public async Task<IEnumerable<T>> SelectAsync<T>(string sqlString, object parameter)
        {
            return await dbConnectionFatory.Connection.QueryAsync<T>(sqlString, parameter, commandTimeout: _commandTimeout);
        }

        public async Task<T> SelectFirstOrDefaultAsync<T>(string sqlString, object parameters)
        {
            return await dbConnectionFatory.Connection.QueryFirstOrDefaultAsync<T>(sqlString, parameters, commandTimeout: _commandTimeout);
        }

        public async Task<List<T>> SelectUserRolesAsync<T>(string sqlString, int UserId)
        {
            var roles = await dbConnectionFatory.Connection.QueryAsync<T>(sqlString, new { UserId = UserId }, commandTimeout: _commandTimeout);
            return roles?.ToList() ?? new List<T>();

        }

        public async Task<T> ExecuteScalarAsync<T>(string sqlString, object Parameters)
        {
            return await dbConnectionFatory.Connection.ExecuteScalarAsync<T>(sqlString, Parameters, commandTimeout: _commandTimeout);
        }

        public async Task<int> ExecuteAsync(string sqlString, object parameter)
        {

            return await dbConnectionFatory.Connection.ExecuteAsync(sqlString, parameter, commandTimeout: _commandTimeout);
        }


    }
}

