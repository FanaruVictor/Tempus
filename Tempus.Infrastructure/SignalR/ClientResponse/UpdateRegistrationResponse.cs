using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.SignalR.ClientResponse;

public class UpdateRegistrationResponse : IClientResponse
{
    public RegistrationOverview Registration { get; set; }
    public Guid GroupId { get; set; }
    public string Message { get; set; }
}