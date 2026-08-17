using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public class ExterneBronRepository : BaseRepository, IExterneBronRepository
{
    public ExterneBronRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<IEnumerable<ExterneBron>> GetAllAsync()
    {
        return await VoerUitInContextAsync(async context =>
            await context.ExterneBronnen
                .OrderByDescending(b => b.ToegevoegdOp)
                .ToListAsync()
        );
    }

    public async Task<ExterneBron?> GetByIdAsync(int id)
    {
        return await VoerUitInContextAsync(async context =>
            await context.ExterneBronnen.FindAsync(id)
        );
    }

    public async Task AddAsync(ExterneBron bron)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.ExterneBronnen.AddAsync(bron);
            await context.SaveChangesAsync();
        });
    }

    public async Task UpdateAsync(ExterneBron bron)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.ExterneBronnen.Update(bron);
            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteAsync(int id)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var bron = await context.ExterneBronnen.FindAsync(id);
            if (bron != null)
            {
                context.ExterneBronnen.Remove(bron);
                await context.SaveChangesAsync();
            }
        });
    }
}