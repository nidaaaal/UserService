
using Microsoft.Data.SqlClient;
using System.Data;
using UserService.Domain.Interfaces.Repositories;

namespace UserService.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;

        public IUserRepository Users { get; }

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            Users = new UserRepository(_connection);
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection.Dispose();
        }
    }

    }
