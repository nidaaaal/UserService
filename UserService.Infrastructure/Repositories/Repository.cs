
using System.Data;
using Dapper;

using UserService.Domain.Interfaces.Repositories;

namespace UserService.Infrastructure.Repositories
{
    public class Repository<T> : BaseRepository, IRepository<T> where T : class
    {
        private readonly string _entityName;

        public Repository(IDbConnection db, IDbTransaction? transaction = null)
            : base(db, transaction)
        {
            _entityName = typeof(T).Name;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.QueryAsync<T>(
                $"spGetAll{_entityName}s",
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _db.QueryFirstOrDefaultAsync<T>(
                $"spGet{_entityName}ById",
                new { Id = id },
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> AddAsync(T entity)
        {
            var rows = await _db.ExecuteAsync(
                $"spAdd{_entityName}",
                entity,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var rows = await _db.ExecuteAsync(
                $"spUpdate{_entityName}",
                entity,
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rows = await _db.ExecuteAsync(
                $"spDelete{_entityName}",
                new { Id = id },
                transaction: _transaction,
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }
    }
}
