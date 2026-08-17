using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public class BerichtRepository : BaseRepository, IBerichtRepository
{
    public BerichtRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<IEnumerable<Bericht>> GetChatGeschiedenisAsync(string gebruikerAId, string gebruikerBId)
    {
        return await VoerUitInContextAsync(async context =>
            await context.Berichten
                .AsNoTracking()
                .Include(b => b.Afzender)
                .Where(b => (b.AfzenderId == gebruikerAId && b.OntvangerId == gebruikerBId) ||
                            (b.AfzenderId == gebruikerBId && b.OntvangerId == gebruikerAId))
                .OrderBy(b => b.VerzondenOp)
                .ToListAsync()
        );
    }

    public async Task AddBerichtAsync(Bericht bericht)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.Berichten.AddAsync(bericht);
            await context.SaveChangesAsync();
        });
    }

    public async Task MarkeerAlsGelezenAsync(string afzenderId, string ontvangerId)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var ongelezen = await context.Berichten
                .Where(b => b.AfzenderId == afzenderId && b.OntvangerId == ontvangerId && !b.IsGelezen)
                .ToListAsync();

            if (ongelezen.Any())
            {
                foreach (var bericht in ongelezen)
                {
                    bericht.IsGelezen = true;
                }
                context.Berichten.UpdateRange(ongelezen);
                await context.SaveChangesAsync();
            }
        });
    }

    public async Task<int> GetAantalOngelezenBerichtenAsync(string ontvangerId)
    {
        return await VoerUitInContextAsync(async context =>
            await context.Berichten.CountAsync(b => b.OntvangerId == ontvangerId && !b.IsGelezen)
        );
    }

    public async Task<int> GetAantalOngelezenBerichtenVanAfzenderAsync(string ontvangerId, string afzenderId)
    {
        return await VoerUitInContextAsync(async context =>
            await context.Berichten.CountAsync(b => b.OntvangerId == ontvangerId && b.AfzenderId == afzenderId && !b.IsGelezen)
        );
    }

    public async Task<IEnumerable<Bericht>> GetBerichtenTussenGebruikersAsync(string userAId, string userBId, DateTime? totDatum = null)
    {
        return await VoerUitInContextAsync(async context =>
        {
            var query = context.Berichten
                .AsNoTracking()
                .Include(b => b.Afzender)
                .Include(b => b.Ontvanger)
                .Where(b => (b.AfzenderId == userAId && b.OntvangerId == userBId) ||
                            (b.AfzenderId == userBId && b.OntvangerId == userAId));

            if (totDatum.HasValue)
            {
                query = query.Where(b => b.VerzondenOp <= totDatum.Value);
            }

            return await query.OrderBy(b => b.VerzondenOp).ToListAsync();
        });
    }
}