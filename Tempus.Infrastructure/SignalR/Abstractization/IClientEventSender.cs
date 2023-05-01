namespace Tempus.Infrastructure.SignalR.Abstractization;

public interface IClientEventSender
{
   public Task SendToUserAsync<T>(T message, string userId);
}