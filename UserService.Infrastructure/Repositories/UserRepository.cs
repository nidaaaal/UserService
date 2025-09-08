using Dapper;
using System.Data;
using UserService.Domain.Entity;
using UserService.Domain.Interfaces.Repositories;
using UserService.Infrastructure.Persistence;
using Microsoft.Extensions;
using UserService.Shared.DTO;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration config) : base(config) { }

        public async Task<int> AddUserAsync(UserRegistrationDto dto)
        {
            using var connection = CreateConnection();

            // Convert Phones to PostgreSQL composite type array
            var phones = dto.Phones.Select(ph => new
            {
                phone_number = ph.PhoneNumber,
                country_id = ph.CountryId,
                is_primary = ph.IsPrimary
            }).ToArray();

            var parameters = new DynamicParameters();
            parameters.Add("p_email", dto.Email);
            parameters.Add("p_password_hash", dto.PasswordHash);
            parameters.Add("p_full_name", dto.FullName);
            parameters.Add("p_date_of_birth", dto.DateOfBirth);
            parameters.Add("p_gender", dto.Gender);
            parameters.Add("p_phones", phones);

            // usp_add_new_user returns INT (status code: 0, -1, -2)
            var result = await connection.ExecuteScalarAsync<int>(
                "usp_add_new_user",
                parameters,
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<User>(
                "core.sp_get_user_by_id", // you should already have this function
                new { p_user_id = userId },
                commandType: CommandType.StoredProcedure);
        }
    }
}
