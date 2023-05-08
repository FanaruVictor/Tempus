using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.SignalR.Abstractization;

public interface IClientEventSender
{
    Task SendRegistrationCreatedEventAsync(RegistrationOverview message, string userId);
    Task SendRegistrationDeleted(Guid registrationId, Guid groupId, string userId);
    Task SendRegistrationUpdated(Guid registrationId, Guid groupId, string userId);
}