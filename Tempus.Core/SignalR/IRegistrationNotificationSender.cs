using Tempus.Core.Models.Registrations;

namespace Tempus.Core.SignalR;

public interface IRegistrationNotificationSender
{
    Task SendAddRegistrationNotification(RegistrationOverview registration);
    Task SendEditRegistrationNotification(string message);
    Task SendDeleteRegistrationNotification(Guid registrationId);
}