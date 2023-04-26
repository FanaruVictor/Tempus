using Microsoft.AspNetCore.SignalR;

namespace Tempus.API.SignalR;

public class UserIdProvider: IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name;
    }
}