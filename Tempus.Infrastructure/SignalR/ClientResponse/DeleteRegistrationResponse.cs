namespace Tempus.Infrastructure.SignalR.ClientResponse;

public class DeleteRegistrationResponse : IClientResponse
{
    public Guid RegistrationId { get; set; }
    public Guid GroupId { get; set; }
}