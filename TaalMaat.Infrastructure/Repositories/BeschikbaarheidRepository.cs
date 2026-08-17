using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public class BeschikbaarheidRepository : BaseRepository, IBeschikbaarheidRepository
{
    public BeschikbaarheidRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<IEnumerable<Beschikbaarheid>> GetForGebruikerAsync(string gebruikerId) =>
        await VoerUitInContextAsync(async context =>
            await context.Beschikbaarheden
                .Where(b => b.GebruikerId == gebruikerId)
                .ToListAsync()
        );

    public async Task AddAsync(Beschikbaarheid beschikbaarheid)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.Beschikbaarheden.AddAsync(beschikbaarheid);
            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteAsync(int id)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var item = await context.Beschikbaarheden.FindAsync(id);
            if (item != null)
            {
                context.Beschikbaarheden.Remove(item);
                await context.SaveChangesAsync();
            }
        });
    }

    public async Task DeleteAllForGebruikerAsync(string gebruikerId)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var items = await context.Beschikbaarheden
                .Where(b => b.GebruikerId == gebruikerId)
                .ToListAsync();
            context.Beschikbaarheden.RemoveRange(items);
            await context.SaveChangesAsync();
        });
    }
}