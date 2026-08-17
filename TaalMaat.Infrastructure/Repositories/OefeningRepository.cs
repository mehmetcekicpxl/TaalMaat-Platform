using Microsoft.EntityFrameworkCore;
using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public class OefeningRepository : BaseRepository, IOefeningRepository
{
    public OefeningRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<Oefening?> GetByIdAsync(int id) =>
        await VoerUitInContextAsync(async context =>
            await context.Oefeningen
                .Include(o => o.Vragen)
                .FirstOrDefaultAsync(o => o.Id == id)
        );

    public async Task<IEnumerable<Oefening>> GetGoedgekeurdeByNiveauAsync(OefeningNiveau niveau) =>
        await VoerUitInContextAsync(async context =>
            await context.Oefeningen
                .Include(o => o.Vragen)
                .Where(o => o.Niveau == niveau && o.IsGoedgekeurd)
                .ToListAsync()
        );

    public async Task<IEnumerable<Oefening>> GetAllAsync() =>
        await VoerUitInContextAsync(async context =>
            await context.Oefeningen
                .Include(o => o.Vragen)
                .OrderByDescending(o => o.AangemaaktOp)
                .ToListAsync()
        );

    public async Task AddAsync(Oefening oefening)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            await context.Oefeningen.AddAsync(oefening);
            await context.SaveChangesAsync();
        });
    }

    public async Task UpdateAsync(Oefening oefening)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            // Haal de bestaande oefening op inclusief vragen (tracked entity)
            var bestaande = await context.Oefeningen
                .Include(o => o.Vragen)
                .FirstOrDefaultAsync(o => o.Id == oefening.Id);

            if (bestaande == null) return;

            // Werk de scalaire velden bij
            bestaande.Titel = oefening.Titel;
            bestaande.Inhoud = oefening.Inhoud;
            bestaande.YouTubeUrl = oefening.YouTubeUrl;
            bestaande.AudioUrl = oefening.AudioUrl;
            bestaande.Niveau = oefening.Niveau;
            bestaande.IsGoedgekeurd = oefening.IsGoedgekeurd;

            // Verwijder alle bestaande vragen en voeg de nieuwe toe
            context.OefeningVragen.RemoveRange(bestaande.Vragen);
            foreach (var vraag in oefening.Vragen)
            {
                bestaande.Vragen.Add(new OefeningVraag
                {
                    VraagTekst = vraag.VraagTekst,
                    OptiesJson = vraag.OptiesJson,
                    JuistAntwoord = vraag.JuistAntwoord
                });
            }

            await context.SaveChangesAsync();
        });
    }

    public async Task DeleteAsync(int id)
    {
        await VoerUitInContextZonderResultaatAsync(async context =>
        {
            var item = await context.Oefeningen.FindAsync(id);
            if (item != null)
            {
                context.Oefeningen.Remove(item);
                await context.SaveChangesAsync();
            }
        });
    }
}