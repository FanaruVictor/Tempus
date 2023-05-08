using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Tempus.Infrastructure.SignalR.ClientResponse;

public sealed class ClientEvent
{
    public ClientEvent(IClientResponse innerResponse, ClientResponseType responseType)
    {
        InnerEventJson = JsonConvert.SerializeObject(innerResponse);
        ResponseType = responseType;
    }

    public string InnerEventJson { get; }

    public ClientResponseType ResponseType { get; }
}

public enum ClientResponseType
{
    AddRegistration,
    DeleteRegistration,
    UpdateRegistration,
}