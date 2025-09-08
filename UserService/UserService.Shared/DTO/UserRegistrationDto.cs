namespace UserService.Shared.DTO;


public class UserRegistrationDto
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public char? Gender { get; set; }
    public List<PhoneDto> Phones { get; set; } = new();
}
