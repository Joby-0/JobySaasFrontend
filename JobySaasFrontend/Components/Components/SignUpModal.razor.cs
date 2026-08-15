using System.ComponentModel.DataAnnotations;
using BlazorBootstrap;
using JobySaasFrontend.Models.DTO;
using JobySaasFrontend.Services;
using Microsoft.AspNetCore.Components;

namespace JobySaasFrontend.Components.Components;

public partial class SignUpModal
{
    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private Modal? modal;
    private readonly SignupModel signupModel = new();
    private string? errorMessage;
    private bool isSubmitting;

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

    private async Task HandleSubmit()
    {
        isSubmitting = true;
        errorMessage = null;

        try
        {
            var confirmationUrl = NavigationManager.ToAbsoluteUri("Account/ConfirmEmail").AbsoluteUri;
            var response = await AuthService.RegisterAsync(new RegisterRequest
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                Password = signupModel.Password,
                ConfirmPassword = signupModel.ConfirmPassword
            }, confirmationUrl);

            if (!response.Succeeded)
            {
                errorMessage = string.Join(" ", response.Errors);
                return;
            }

            await CloseModal();
            NavigationManager.NavigateTo($"Account/RegisterConfirmation?email={Uri.EscapeDataString(signupModel.Email)}");
        }
        finally
        {
            isSubmitting = false;
        }
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
