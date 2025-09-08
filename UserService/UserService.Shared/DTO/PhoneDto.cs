
namespace UserService.Shared.DTO;

public class PhoneDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public short CountryId { get; set; }
    public bool IsPrimary { get; set; }
}
