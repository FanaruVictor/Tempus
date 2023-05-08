namespace Tempus.Infrastructure.SignalR.ClientResponse;

public class UpdateRegistrationResponse : IClientResponse
{
    public Guid RegistrationId { get; set; }
    public Guid GroupId { get; set; }
    public string Message { get; set; }
}