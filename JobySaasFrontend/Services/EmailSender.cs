using JobySaasFrontend.Data;
using Microsoft.AspNetCore.Identity;
using Resend;

public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly ResendClient _resend;

    public EmailSender(ResendClient resend)
    {
        _resend = resend;
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var message = new EmailMessage();

        message.From = "Joby <onboarding@resend.dev>";
        message.To.Add(email);
        message.Subject = "Confirm your Joby account";

        message.HtmlBody = $"""
            <h2>Welcome to Joby!</h2>

            <p>Thanks for creating an account.</p>

            <p>Please confirm your email address by clicking the button below:</p>

            <p>
                <a href="{confirmationLink}"
                   style="display:inline-block;
                          padding:12px 20px;
                          background:#1f5a2a;
                          color:white;
                          text-decoration:none;
                          border-radius:6px;">
                    Confirm Email
                </a>
            </p>

            <p>If you didn't create this account, you can safely ignore this email.</p>
            """;

        await _resend.EmailSendAsync(message);
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}