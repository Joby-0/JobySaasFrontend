namespace JobySaasFrontend.Models.DTO;

public sealed class ConfirmEmailRequest
{
    public string UserId { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;
}
