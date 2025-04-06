using API.Hubs;
using Aplication.Interfaces.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace API.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<HubRequest> _hubContext;
    
    public NotificationService(IHubContext<HubRequest> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task SendAcceptedOfferNotification(string postId, string offerId)
    {
        Console.WriteLine($"Enviando notificación al grupo {postId}");
        await _hubContext.Clients.Group(postId).SendAsync("GetOfferState", new {
            PostId = postId,
            OfferId = offerId,
            Message = $"La oferta {offerId} ha sido aceptada. Para el Post {postId}."
        });
    }
}