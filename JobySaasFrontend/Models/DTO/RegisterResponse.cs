namespace JobySaasFrontend.Models.DTO;

public class RegisterResponse
{
    public bool Succeeded { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = [];

    public static RegisterResponse Success() => new() { Succeeded = true };

    public static RegisterResponse Failure(IEnumerable<string> errors) => new()
    {
        Errors = errors.ToArray()
    };
}

public class ApiRegisterResponse
{
   public bool Success { get; set; }
   public string Message { get; set; }
}

