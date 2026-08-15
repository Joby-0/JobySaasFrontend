using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using JobySaasFrontend.Data;
using JobySaasFrontend.Models.DTO;

namespace JobySaasFrontend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserStore<ApplicationUser> userStore;
    private readonly IEmailSender<ApplicationUser> emailSender;

    public AuthService(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore,IEmailSender<ApplicationUser> emailSender)
    {
        this.userManager = userManager;
        this.userStore = userStore;
        this.emailSender = emailSender;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, string confirmationUrl)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The configured Identity store does not support email addresses.");
        }

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);
        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        await emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);

        // CreateAsync writes the user to ApplicationDbContext. Do not send an email before it succeeds.
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return RegisterResponse.Failure(result.Errors.Select(error => error.Description));
        }

        var userId = await userManager.GetUserIdAsync(user);
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        Console.Write(code);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = QueryHelpers.AddQueryString(confirmationUrl, new Dictionary<string, string?>
            {
                ["userId"] = userId,
                ["code"] = code
            });

        await emailSender.SendConfirmationLinkAsync(user,request.Email, HtmlEncoder.Default.Encode(callbackUrl));

        return RegisterResponse.Success();
    }
}
