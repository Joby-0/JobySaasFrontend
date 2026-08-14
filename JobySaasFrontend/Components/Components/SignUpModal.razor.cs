using System.ComponentModel.DataAnnotations;
using BlazorBootstrap;

namespace JobySaasFrontend.Components.Components;

public partial class SignUpModal
{
    private Modal? modal;
    private readonly SignupModel signupModel = new();

    public async Task OpenModal()
    {
        if (modal is not null)
        {
            await modal.ShowAsync();
        }
    }

    private async Task CloseModal()
    {
        if (modal is not null)
        {
            await modal.HideAsync();
        }
    }

    private Task HandleSubmit()
    {
        // Placeholder: connect the validated model to the signup service later.
        return Task.CompletedTask;
    }

    private sealed class SignupModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
