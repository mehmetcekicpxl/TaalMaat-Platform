using Microsoft.AspNetCore.SignalR;
using TaalMaat.Core.Interfaces;
using TaalMaat.Infrastructure.Hubs;

namespace TaalMaat.Infrastructure.Services;

/// <summary>
/// Concrete implementatie van INotificatieService via SignalR NotificatieHub
/// </summary>
public class NotificatieService : INotificatieService
{
    private readonly IHubContext<NotificatieHub> _hubContext;

    public NotificatieService(IHubContext<NotificatieHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task StuurNieuwBuddyVerzoekAsync(string ontvangerGebruikerId, object verzoekData)
    {
        await _hubContext.Clients
            .Group(NotificatieHub.GetGroupName(ontvangerGebruikerId))
            .SendAsync("NieuwBuddyVerzoek", verzoekData);
    }

    public async Task StuurBuddyVerzoekGeaccepteerdAsync(string verzenderGebruikerId, object data)
    {
        await _hubContext.Clients
            .Group(NotificatieHub.GetGroupName(verzenderGebruikerId))
            .SendAsync("BuddyVerzoekGeaccepteerd", data);
    }

    public async Task StuurBuddyVerzoekAfgewezenAsync(string verzenderGebruikerId, object data)
    {
        await _hubContext.Clients
            .Group(NotificatieHub.GetGroupName(verzenderGebruikerId))
            .SendAsync("BuddyVerzoekAfgewezen", data);
    }

    public async Task StuurAccountStatusUpdateAsync(string gebruikerId, string type)
    {
        await _hubContext.Clients
            .Group(NotificatieHub.GetGroupName(gebruikerId))
            .SendAsync("OntvangStatusUpdate", gebruikerId, type);
    }
    public async Task StuurNieuwChatBerichtAsync(string ontvangerId, object berichtData)
    {
        await _hubContext.Clients
            .Group(NotificatieHub.GetGroupName(ontvangerId))
            .SendAsync("NieuwChatBericht", berichtData);
    }
}
