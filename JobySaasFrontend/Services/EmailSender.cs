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
                <!DOCTYPE html>
                <html lang="en">

                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Confirm your Joby account</title>
                </head>

                <body style="
                    margin:0;
                    padding:0;
                    background-color:#F4F1DE;
                    font-family:Arial, Helvetica, sans-serif;
                    color:#3D405B;
                ">

                    <table width="100%" cellpadding="0" cellspacing="0" border="0"
                        style="
                            background-color:#F4F1DE;
                            padding:50px 20px;
                        ">
                        <tr>
                            <td align="center">

                                <table width="100%" cellpadding="0" cellspacing="0" border="0"
                                    style="
                                        max-width:600px;
                                        background-color:#ffffff;
                                        border-radius:14px;
                                        overflow:hidden;
                                    ">

                                    <!-- Header -->
                                    <tr>
                                        <td align="center"
                                            style="
                                                padding:30px 40px;
                                                background-color:#3D405B;
                                            ">

                                            <div style="
                                                font-size:30px;
                                                font-weight:700;
                                                color:#ffffff;
                                                letter-spacing:-1px;
                                            ">
                                                Joby
                                            </div>

                                        </td>
                                    </tr>

                                    <!-- Accent -->
                                    <tr>
                                        <td style="
                                            height:5px;
                                            background-color:#81B29A;
                                            font-size:0;
                                            line-height:0;
                                        ">
                                            &nbsp;
                                        </td>
                                    </tr>

                                    <!-- Content -->
                                    <tr>
                                        <td style="
                                            padding:45px 40px;
                                        ">

                                            <h1 style="
                                                margin:0 0 20px 0;
                                                font-size:28px;
                                                line-height:1.3;
                                                color:#3D405B;
                                            ">
                                                Welcome to Joby!
                                            </h1>

                                            <p style="
                                                margin:0 0 16px 0;
                                                font-size:16px;
                                                line-height:1.6;
                                                color:#555555;
                                            ">
                                                Thanks for creating your Joby account.
                                            </p>

                                            <p style="
                                                margin:0 0 30px 0;
                                                font-size:16px;
                                                line-height:1.6;
                                                color:#555555;
                                            ">
                                                You're almost ready to get started.
                                                Please confirm your email address by clicking
                                                the button below.
                                            </p>

                                            <!-- Button -->
                                            <table cellpadding="0"
                                                cellspacing="0"
                                                border="0"
                                                style="margin-bottom:30px;">
                                                <tr>
                                                    <td style="
                                                        background-color:#E07A5F;
                                                        border-radius:8px;
                                                    ">
                                                        <a href="{confirmationLink}"
                                                        style="
                                                            display:inline-block;
                                                            padding:14px 26px;
                                                            font-size:16px;
                                                            font-weight:bold;
                                                            color:#ffffff;
                                                            text-decoration:none;
                                                            border-radius:8px;
                                                        ">
                                                            Confirm your email
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>

                                            <!-- Fallback -->
                                            <p style="
                                                margin:0 0 10px 0;
                                                font-size:14px;
                                                line-height:1.5;
                                                color:#777777;
                                            ">
                                                Having trouble with the button?
                                            </p>

                                            <p style="
                                                margin:0 0 30px 0;
                                                font-size:13px;
                                                line-height:1.5;
                                                word-break:break-all;
                                            ">
                                                <a href="{confirmationLink}"
                                                style="
                                                    color:#3D405B;
                                                    text-decoration:underline;
                                                ">
                                                    {confirmationLink}
                                                </a>
                                            </p>

                                            <!-- Divider -->
                                            <table width="100%"
                                                cellpadding="0"
                                                cellspacing="0"
                                                border="0">
                                                <tr>
                                                    <td style="
                                                        border-top:1px solid #eeeeee;
                                                    ">
                                                    </td>
                                                </tr>
                                            </table>

                                            <p style="
                                                margin:24px 0 0 0;
                                                font-size:13px;
                                                line-height:1.5;
                                                color:#888888;
                                            ">
                                                If you didn't create a Joby account,
                                                you can safely ignore this email.
                                            </p>

                                        </td>
                                    </tr>

                                    <!-- Footer -->
                                    <tr>
                                        <td align="center"
                                            style="
                                                padding:25px 40px;
                                                background-color:#fafafa;
                                            ">

                                            <p style="
                                                margin:0;
                                                font-size:12px;
                                                color:#999999;
                                            ">
                                                © {DateTime.UtcNow.Year} Joby.
                                                All rights reserved.
                                            </p>

                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>

                </body>
                </html>
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