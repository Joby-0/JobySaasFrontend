using System.ComponentModel.DataAnnotations;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace JobySaasFrontend.Components.Components;

public partial class LoginModal
{
    [Parameter] public EventCallback OnRegisterRequested { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private Modal? modal;
    private readonly LoginModel loginModel = new();
    private string? errorMessage;

    public async Task OpenModal()
    {
        if (modal is not null)
        {
            errorMessage = GetLoginError();
            await modal.ShowAsync();
        }
    }

    private async Task CloseModal()
    {
        if (modal is not null)
            await modal.HideAsync();
    }

    private async Task OpenRegister()
    {
        await CloseModal();
        await OnRegisterRequested.InvokeAsync();
    }

    private string? GetLoginError()
    {
        var query = QueryHelpers.ParseQuery(new Uri(NavigationManager.Uri).Query);
        return query.TryGetValue("loginError", out var error)
            ? error.ToString() switch
            {
                "confirm-email" => "You must confirm your email address before signing in.",
                _ => "Invalid email or password."
            }
            : null;
    }

    private sealed class LoginModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

}
