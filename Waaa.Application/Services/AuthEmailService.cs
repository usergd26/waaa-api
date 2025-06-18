using Microsoft.AspNetCore.Identity;

namespace Waaa.Application.Services
{
    public class AuthEmailService : IEmailSender<IdentityUser>
    {
        Task IEmailSender<IdentityUser>.SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
        {
            Console.WriteLine($"Confirmation link: {confirmationLink}");
            return Task.CompletedTask;
        }

        Task IEmailSender<IdentityUser>.SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
        {
            Console.WriteLine($"Reset code: {resetCode}");
            return Task.CompletedTask;
        }

        Task IEmailSender<IdentityUser>.SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
        {
            Console.WriteLine($"Reset linki: {resetLink}");
            return Task.CompletedTask;
        }
    }
}
