namespace Aplication.Interfaces.Helpers;

public interface INotificationService
{
    /// Envia una notificación al usuario cuando se acepta su oferta
    Task SendAcceptedOfferNotification(string postId, string offerId);
}