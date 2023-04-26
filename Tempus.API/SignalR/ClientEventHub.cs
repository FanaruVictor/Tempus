using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Tempus.API.SignalR;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ClientEventHub : Hub
{

    private static readonly ConnectionManager Connections = new();

    // public override Task OnConnectedAsync()
    // {
    //     var name = Context.User.Identity.Name;
    //
    //     Connections.RegisterConnection(name, Context.ConnectionId);
    //     return base.OnConnectedAsync();
    // }
    
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var name = Context.User.Identity.Name;

        Connections.RemoveConnection(name, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}