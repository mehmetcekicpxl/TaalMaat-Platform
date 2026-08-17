using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

/// <summary>
/// Concrete implementatie van IBuddyRepository.
/// Bevat de kritieke bedrijfsregel: maximaal 3 actieve buddies per Vrijwilliger.
/// </summary>
public class BuddyRepository : BaseRepository, IBuddyRepository
{
    public BuddyRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    // === BuddyVerzoek methoden ===

    public async Task<BuddyVerzoek?> GetVerzoekByIdAsync(int id) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyVerzoeken
                .Include(v => v.Verzender)
                .Include(v => v.Ontvanger)
                .FirstOrDefaultAsync(v => v.Id == id)
        );

    public async Task<IEnumerable<BuddyVerzoek>> GetVerzoekForOntvangerAsync(string ontvangerId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyVerzoeken
                .Include(v => v.Verzender)
                .Where(v => v.OntvangerId == ontvangerId)
                .OrderByDescending(v => v.AangemaaktOp)
                .ToListAsync()
        );

    public async Task<IEnumerable<BuddyVerzoek>> GetVerzoekForVerzenderAsync(string verzenderId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyVerzoeken
                .Include(v => v.Ontvanger)
                .Where(v => v.VerzenderId == verzenderId)
                .ToListAsync()
        );

    public async Task AddVerzoekAsync(BuddyVerzoek verzoek)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.BuddyVerzoeken.AddAsync(verzoek);
            await context.SaveChangesAsync();
        });
    }

    public async Task UpdateVerzoekAsync(BuddyVerzoek verzoek)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.BuddyVerzoeken.Update(verzoek);
            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteVerzoekAsync(int verzoekId)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var verzoek = await context.BuddyVerzoeken.FindAsync(verzoekId);
            if (verzoek != null)
            {
                context.BuddyVerzoeken.Remove(verzoek);
                await context.SaveChangesAsync();
            }
        });
    }

    // === BuddyKoppeling methoden ===

    public async Task<IEnumerable<BuddyKoppeling>> GetActieveKoppelingenForVrijwilligerAsync(string vrijwilligerId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyKoppelingen
                .Include(k => k.Anderstalig)
                .Where(k => k.VrijwilligerId == vrijwilligerId && k.IsActief)
                .ToListAsync()
        );

    public async Task<IEnumerable<BuddyKoppeling>> GetActieveKoppelingenForAnderstaligAsync(string anderstaligId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyKoppelingen
                .Include(k => k.Vrijwilliger)
                .Where(k => k.AnderstaligId == anderstaligId && k.IsActief)
                .ToListAsync()
        );

    public async Task AddKoppelingAsync(BuddyKoppeling koppeling)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.BuddyKoppelingen.AddAsync(koppeling);
            await context.SaveChangesAsync();
        });
    }

    public async Task<BuddyKoppeling?> GetKoppelingAsync(string vrijwilligerId, string anderstaligId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyKoppelingen
                .FirstOrDefaultAsync(k => k.VrijwilligerId == vrijwilligerId && k.AnderstaligId == anderstaligId && k.IsActief)
        );

    public async Task UpdateKoppelingAsync(BuddyKoppeling koppeling)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.BuddyKoppelingen.Update(koppeling);
            await context.SaveChangesAsync();
        });
    }

    /// <summary>
    /// Telt het aantal actieve buddies. Gebruikt voor de max-3 bedrijfsregel.
    /// </summary>
    public async Task<int> GetAantalActieveBuddiesAsync(string vrijwilligerId) =>
        await VoerUitInContextAsync(async context =>
            await context.BuddyKoppelingen
                .CountAsync(k => k.VrijwilligerId == vrijwilligerId && k.IsActief)
        );
}