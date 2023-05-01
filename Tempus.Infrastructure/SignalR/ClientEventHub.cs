using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.Infrastructure.SignalR;

[Authorize]
public class ClientEventHub : Hub
{

    private readonly IConnectionManager _connections;
    private readonly IUserInfo _user;
    
    public ClientEventHub(IUserInfo user)
    {
        _user = user;
        _connections = new ConnectionManager();
    }

    public override Task OnConnectedAsync()
    {
        var name = _user.Id;
    
        _connections.RegisterConnection(name, Context.ConnectionId);
        return base.OnConnectedAsync();
    }
    
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var name = Context.User.Identity.Name;

        _connections.RemoveConnection(name, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}