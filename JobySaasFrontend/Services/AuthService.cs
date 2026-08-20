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
    private readonly IAuthApiClient authApiClient;

    public AuthService(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IEmailSender<ApplicationUser> emailSender, IAuthApiClient authApiClient)
    {
        this.userManager = userManager;
        this.userStore = userStore;
        this.emailSender = emailSender;
        this.authApiClient = authApiClient;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, string confirmationUrl)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The configured Identity store does not support email addresses.");
        }
        if(request.ConfirmPassword != request.Password)
        {
            throw new NotSupportedException("The password thoes not match");
        }

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await userStore.SetUserNameAsync(user, request.Email, CancellationToken.None);

        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;

        await emailStore.SetEmailAsync(user, request.Email, CancellationToken.None);

        // 1. Create user in Blazor/Identity database
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return RegisterResponse.Failure(result.Errors.Select(error => error.Description));
        }
        var userId = await userManager.GetUserIdAsync(user);
        // 2. Create user in API database
        // var apiResponse = await authApiClient.RegisterAsync(new ApiRegisterRequest
        // {
        //     UserId = userId!,
        //     FirstName = request.FirstName,
        //     LastName = request.LastName,
        //     Email = request.Email,
        //     Password = request.Password
            
        // });

        // if (!apiResponse.Success)
        // {
        //     // API registration failed.
        //     // Remove the Identity user so we don't leave
        //     // a partially registered account behind.
        //     await userManager.DeleteAsync(user);

        //     return RegisterResponse.Failure(new[] { "The account could not be created." });
        // }

        // 3. Generate Identity email confirmation token

        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        // 4. Create confirmation URL
        var callbackUrl = QueryHelpers.AddQueryString(confirmationUrl, new Dictionary<string, string?>
        {
            ["userId"] = userId,
            ["code"] = code
        });

        // 5. Send confirmation email
        await emailSender.SendConfirmationLinkAsync(user, request.Email, HtmlEncoder.Default.Encode(callbackUrl));

        // 6. Everything succeeded
        return RegisterResponse.Success();
    }
}
