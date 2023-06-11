﻿namespace Tempus.Core.IServices.Auth;

public class LoginCredentials
{
    public string Email { get; init; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string PhotoURL { get; set; }
    public string ExternalId { get; set; }
}