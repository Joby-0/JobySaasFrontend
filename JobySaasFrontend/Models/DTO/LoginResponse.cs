using JobySaasFrontend.Encryption;

namespace JobySaasFrontend.Models.DTO;

public class LoginResponse
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public string Email {get;set;}
    public JwtUserToken JwtToken { get; set; }
}