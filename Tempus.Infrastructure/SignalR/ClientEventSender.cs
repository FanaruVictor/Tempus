using System.Collections;
using Microsoft.AspNetCore.SignalR;
using Tempus.Infrastructure.SignalR.Abstractization;

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
    
    public async Task SendToUserAsync<T>(T message, string userId)
    {
        var connections = _signalRConnection.GetConnections(userId);
        await SendToConnectionsAsync(message, connections);
    }

    private async Task SendToConnectionsAsync<T>(T message, IEnumerable<string> connections)
    {
        if(connections == null)
        {
            return;
        }

        foreach(var connection in connections)
        {
            await _hubContext.Clients.Client(connection).SendAsync("client-events", message);
        }
    }
}