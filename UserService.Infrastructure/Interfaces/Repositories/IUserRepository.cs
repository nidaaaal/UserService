
using UserService.Shared.DTO;
using UserService.Domain.Entity;

namespace UserService.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> AddUserAsync(UserRegistrationDto userRegistrationDto);
        Task<User?> GetByEmailAsync(string email);
    }
}
