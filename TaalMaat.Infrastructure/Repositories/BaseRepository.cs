using Microsoft.EntityFrameworkCore;
using TaalMaat.Infrastructure.Data;

namespace TaalMaat.Infrastructure.Repositories;

public abstract class BaseRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    protected BaseRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    // Voert een database-actie uit binnen een veilige, geïsoleerde context en retourneert een resultaat
    protected async Task<T> VoerUitInContextAsync<T>(Func<ApplicationDbContext, Task<T>> actie)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await actie(context);
    }

    // Voert een database-actie uit zonder een resultaat te retourneren (bvb. Add, Update, Delete)
    protected async Task VoerUitInContextZonderResultaatAsync(Func<ApplicationDbContext, Task> actie)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        await actie(context);
    }
}