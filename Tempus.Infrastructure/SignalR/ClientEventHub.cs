using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.Infrastructure.SignalR;

[Authorize]
public class ClientEventHub : Hub
{
    private readonly IConnectionManager _connections;

    public ClientEventHub()
    {
        _connections = new ConnectionManager();
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

        _connections.RegisterConnection(userId, Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var name = Context.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

        _connections.RemoveConnection(name, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}