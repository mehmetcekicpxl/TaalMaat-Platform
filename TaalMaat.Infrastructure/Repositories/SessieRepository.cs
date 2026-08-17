using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

/// <summary>
/// Concrete implementatie van ISessieRepository
/// </summary>
public class SessieRepository : BaseRepository, ISessieRepository
{
    public SessieRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<Sessie?> GetByIdAsync(int id) =>
        await VoerUitInContextAsync(async context =>
            await context.Sessies
                .Include(s => s.Vrijwilliger)
                .Include(s => s.Anderstalig)
                .FirstOrDefaultAsync(s => s.Id == id)
        );

    public async Task<IEnumerable<Sessie>> GetForVrijwilligerAsync(string vrijwilligerId) =>
        await VoerUitInContextAsync(async context =>
            await context.Sessies
                .Include(s => s.Anderstalig)
                .Where(s => s.VrijwilligerId == vrijwilligerId)
                .OrderByDescending(s => s.GeplandOp)
                .ToListAsync()
        );

    public async Task<IEnumerable<Sessie>> GetForAnderstaligAsync(string anderstaligId) =>
        await VoerUitInContextAsync(async context =>
            await context.Sessies
                .Include(s => s.Vrijwilliger)
                .Where(s => s.AnderstaligId == anderstaligId)
                .OrderByDescending(s => s.GeplandOp)
                .ToListAsync()
        );

    public async Task AddAsync(Sessie sessie)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.Sessies.AddAsync(sessie);
            await context.SaveChangesAsync();
        });
    }

    public async Task UpdateAsync(Sessie sessie)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.Sessies.Update(sessie);
            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteAsync(int id)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var item = await context.Sessies.FindAsync(id);
            if (item != null)
            {
                context.Sessies.Remove(item);
                await context.SaveChangesAsync();
            }
        });
    }
}