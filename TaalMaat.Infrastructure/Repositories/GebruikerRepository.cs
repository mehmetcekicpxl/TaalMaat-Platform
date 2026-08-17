using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

/// <summary>
/// Concrete implementatie van IGebruikerRepository
/// </summary>
public class GebruikerRepository : BaseRepository, IGebruikerRepository
{
    public GebruikerRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<ApplicationUser?> GetByIdAsync(string id) =>
        await VoerUitInContextAsync(async context =>
            await context.Users
                .Include(u => u.Gemeente)
                .Include(u => u.Beschikbaarheden)
                .FirstOrDefaultAsync(u => u.Id == id)
        );

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
        await VoerUitInContextAsync(async context =>
            await context.Users
                .Include(u => u.Gemeente)
                .Include(u => u.Beschikbaarheden)
                .ToListAsync()
        );

    public async Task<IEnumerable<ApplicationUser>> GetByRolAsync(GebruikerRol rol) =>
        await VoerUitInContextAsync(async context =>
            await context.Users
                .Include(u => u.Beschikbaarheden)
                .Where(u => u.Rol == rol)
                .ToListAsync()
        );

    public async Task UpdateAsync(ApplicationUser gebruiker)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.Users.Update(gebruiker);
            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteAsync(string id)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            // Harde verwijdering conform GDPR Recht op Vergetelheid
            var gebruiker = await context.Users.FindAsync(id);
            if (gebruiker != null)
            {
                // Verwijder gerelateerde data met Restrict policy handmatig
                var berichten = await context.Berichten.Where(b => b.AfzenderId == id || b.OntvangerId == id).ToListAsync();
                context.Berichten.RemoveRange(berichten);

                var verzoeken = await context.BuddyVerzoeken.Where(v => v.VerzenderId == id || v.OntvangerId == id).ToListAsync();
                context.BuddyVerzoeken.RemoveRange(verzoeken);

                var koppelingen = await context.BuddyKoppelingen.Where(k => k.VrijwilligerId == id || k.AnderstaligId == id).ToListAsync();
                context.BuddyKoppelingen.RemoveRange(koppelingen);

                var sessies = await context.Sessies.Where(s => s.VrijwilligerId == id || s.AnderstaligId == id).ToListAsync();
                context.Sessies.RemoveRange(sessies);

                context.Users.Remove(gebruiker);
                await context.SaveChangesAsync();
            }
        });
    }
}