using Microsoft.AspNetCore.Identity;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;
using TaalMaat.Core.Interfaces;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor gebruikersbeheer inclusief GDPR Recht op Vergetelheid
/// </summary>
public class GebruikerService
{
    private readonly IGebruikerRepository _gebruikerRepo;
    private readonly UserManager<ApplicationUser> _userManager;
   

    public GebruikerService(
        IGebruikerRepository gebruikerRepo, 
        UserManager<ApplicationUser> userManager
        )
    {
        _gebruikerRepo = gebruikerRepo;
        _userManager = userManager;
        
    }

    public async Task<ApplicationUser?> GetByIdAsync(string id) =>
        await _gebruikerRepo.GetByIdAsync(id);

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
        await _gebruikerRepo.GetAllAsync();

    public async Task<IEnumerable<ApplicationUser>> GetByRolAsync(GebruikerRol rol) =>
        await _gebruikerRepo.GetByRolAsync(rol);

    public async Task UpdateProfielAsync(ApplicationUser gebruiker) =>
        await _gebruikerRepo.UpdateAsync(gebruiker);

    /// <summary>
    /// Harde verwijdering conform GDPR Recht op Vergetelheid.
    /// Verwijdert de gebruiker volledig uit de database.
    /// </summary>
    public async Task<IdentityResult> DeleteAccountAsync(string gebruikerId)
    {
        var gebruiker = await _userManager.FindByIdAsync(gebruikerId);
        if (gebruiker == null) return IdentityResult.Failed(new IdentityError { Description = "Gebruiker niet gevonden." });
        
        await _gebruikerRepo.DeleteAsync(gebruikerId);
        return IdentityResult.Success;
    }

    /// <summary>
    /// Schorst een gebruikersaccount (IsActief = false)
    /// </summary>
    public async Task SchorsAccountAsync(string gebruikerId)
    {
        var gebruiker = await _gebruikerRepo.GetByIdAsync(gebruikerId);
        if (gebruiker != null)
        {
            gebruiker.IsActief = false;
            await _gebruikerRepo.UpdateAsync(gebruiker);
        }
    }

    /// <summary>
    /// Activeert een geschorst gebruikersaccount (IsActief = true)
    /// </summary>
    public async Task ActiveerAccountAsync(string gebruikerId)
    {
        var gebruiker = await _gebruikerRepo.GetByIdAsync(gebruikerId);
        if (gebruiker != null)
        {
            gebruiker.IsActief = true;
            await _gebruikerRepo.UpdateAsync(gebruiker);
        }
    }

    /// <summary>
    /// Keurt een Vrijwilliger-account goed (IsGeaccepteerd = true)
    /// </summary>
    public async Task KeurGoedeAsync(string gebruikerId)
    {
        var gebruiker = await _gebruikerRepo.GetByIdAsync(gebruikerId);
        if (gebruiker != null)
        {
            gebruiker.IsGeaccepteerd = true;
            gebruiker.IsAfgekeurd = false;
            await _gebruikerRepo.UpdateAsync(gebruiker);
           
        }
    }

    /// <summary>
    /// Keurt een Vrijwilliger-account af (IsAfgekeurd = true) zodat de gebruiker de melding kan zien
    /// </summary>
    public async Task KeurAfAsync(string gebruikerId)
    {
        var gebruiker = await _gebruikerRepo.GetByIdAsync(gebruikerId);
        if (gebruiker != null)
        {
            gebruiker.IsAfgekeurd = true;
            gebruiker.IsGeaccepteerd = false;
            await _gebruikerRepo.UpdateAsync(gebruiker);
        }
    }
}
