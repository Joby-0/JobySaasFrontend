using System.ComponentModel.DataAnnotations;
using JobySaasFrontend.Models.DTO;
using JobySaasFrontend.Services;
using Microsoft.AspNetCore.Components;

namespace JobySaasFrontend.Components.Account.Pages;

public partial class Register
{
    [Inject] private IAuthService AuthService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private readonly RegisterModel registerModel = new();
    private string? errorMessage;
    private string? registeredEmail;
    private bool showConfirmation;
    private bool isSubmitting;

    private async Task HandleSubmit()
    {
        isSubmitting = true;
        errorMessage = null;

        try
        {
            var confirmationUrl = NavigationManager.ToAbsoluteUri("account/confirm-email").AbsoluteUri;
            var nameParts = registerModel.FullName.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length == 0)
            {
                errorMessage = "Full name is required.";
                return;
            }

            var response = await AuthService.RegisterAsync(new RegisterRequest
            {
                FirstName = nameParts[0],
                LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty,
                Email = registerModel.Email,
                Password = registerModel.Password,
                ConfirmPassword = registerModel.ConfirmPassword
            }, confirmationUrl);

            if (!response.Succeeded)
            {
                errorMessage = string.Join(" ", response.Errors);
                return;
            }

            registeredEmail = registerModel.Email;
            showConfirmation = true;
        }
        finally
        {
            isSubmitting = false;
        }
    }

    private sealed class RegisterModel
    {
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;

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
