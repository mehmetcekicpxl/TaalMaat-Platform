using Microsoft.AspNetCore.Identity;
using TaalMaat.Core.Entities;

namespace TaalMaat.WebUI.Components.Account;

/// <summary>
/// Helper om de huidige ingelogde ApplicationUser op te halen
/// </summary>
internal sealed class IdentityUserAccessor(
    UserManager<ApplicationUser> userManager,
    IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser",
                $"Fout: Kan de gebruiker niet laden met ID '{userManager.GetUserId(context.User)}'.",
                context);
        }

        return user!;
    }
}
