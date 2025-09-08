

namespace UserService.Shared.Contracts.Interface
{
    internal interface IPasswordHasher
    {
            string Hash(string password);
            bool Verify(string password, string hash);
    }
}
