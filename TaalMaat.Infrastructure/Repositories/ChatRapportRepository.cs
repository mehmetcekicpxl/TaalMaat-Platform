using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public class ChatRapportRepository : BaseRepository, IChatRapportRepository
{
    public ChatRapportRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task AddRapportAsync(ChatRapport rapport)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.ChatRapporten.AddAsync(rapport);
            await context.SaveChangesAsync();
        });
    }

    public async Task<IEnumerable<ChatRapport>> GetRapportenAsync()
    {
        return await VoerUitInContextAsync(async context =>
            await context.ChatRapporten
                .Include(r => r.Rapporteerder)
                .Include(r => r.Gerapporteerde)
                .OrderByDescending(r => r.RapportageDatum)
                .ToListAsync()
        );
    }

    public async Task<ChatRapport?> GetByIdAsync(int id)
    {
        return await VoerUitInContextAsync(async context =>
            await context.ChatRapporten.FindAsync(id)
        );
    }

    public async Task UpdateAsync(ChatRapport rapport)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            context.ChatRapporten.Update(rapport);
            await context.SaveChangesAsync();
        });
    }
}