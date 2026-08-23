using JobySaasFrontend.Encryption;

public class LoginResponse
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public JwtUserToken JwtToken { get; set; }
}