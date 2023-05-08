using Microsoft.AspNetCore.SignalR;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.SignalR.Abstractization;
using Tempus.Infrastructure.SignalR.ClientResponse;

namespace Tempus.Infrastructure.SignalR;

public class ClientEventSender : IClientEventSender
{
    private readonly IHubContext<ClientEventHub> _hubContext;
    private readonly IConnectionManager _signalRConnection;

    public ClientEventSender(IHubContext<ClientEventHub> hubContext, IConnectionManager signalRConnection)
    {
        _hubContext = hubContext;
        _signalRConnection = signalRConnection;
    }

    public async Task SendRegistrationCreatedEventAsync(RegistrationOverview registration, string userId)
    {
        var connections = _signalRConnection.GetConnections(userId);

        var response = GenericMapper<RegistrationOverview, AddRegistrationResponse>.Map(registration);
        await SendToConnectionsAsync(response, connections,
            ClientResponseType.AddRegistration);
    }

    public async Task SendRegistrationDeleted(Guid registrationId, Guid groupId, string userId)
    {
        var connections = _signalRConnection.GetConnections(userId);
        var response = new DeleteRegistrationResponse
        {
            RegistrationId = registrationId,
            GroupId = groupId,
        };

        await SendToConnectionsAsync(response, connections, ClientResponseType.DeleteRegistration);
    }

    public async Task SendRegistrationUpdated(Guid registrationId, Guid groupId, string userId)
    {
        var connections = _signalRConnection.GetConnections(userId);
        var response = new UpdateRegistrationResponse
        {
            RegistrationId = registrationId,
            GroupId = groupId,
            Message = "One member of your group has already updated this registration please refresh the page."
        };
        await SendToConnectionsAsync(response, connections, ClientResponseType.UpdateRegistration);
    }

    private async Task SendToConnectionsAsync(IClientResponse message, IEnumerable<string> connections,
        ClientResponseType responseType)
    {
        if (connections == null)
        {
            return;
        }

        foreach (var connection in connections)
        {
            var appEvent = new ClientEvent(message, responseType);
            await _hubContext.Clients.Client(connection).SendAsync("client-events", appEvent);
        }
    }
}