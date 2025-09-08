using Dapper;
using Npgsql;
using System.Data;

namespace UserService.Infrastructure.Persistence
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        protected async Task<T?> QuerySingleAsync<T>(string spName, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(
                spName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string spName, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(
                spName, parameters, commandType: CommandType.StoredProcedure);
        }

        protected async Task<int> ExecuteAsync(string spName, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
