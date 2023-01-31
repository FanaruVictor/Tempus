using System.Text;
using Newtonsoft.Json;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Commands.Registrations.Create;
using Tempus.Infrastructure.Commands.Registrations.Update;
using Tempus.Infrastructure.Models.Registrations;
using Tempus.IntegrationTests.Configuration;

namespace Tempus.IntegrationTests.Controllers;

public class RegistrationControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RegistrationControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task When_CallRegistrationsControllerActionCreate_ItShould_ReturnCreatedRegistrationDetails()
    {
        var request = new
        {
            Url = "api/v1/registrations",
            Body = new CreateRegistrationCommand
            {
                CategoryId = new Guid("218e7d32-4ab0-47fb-aae5-b10b309163e3"),
                Content = "content1",
                Title = "title1",
            }
        };
        var content = SerializeContent(JsonConvert.SerializeObject(request.Body));

        var response = await _client.PostAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<BaseRegistration>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual?.Id);
        Assert.Equal("content1", actual.Content);
        Assert.Equal(request.Body.Title, actual.Title);
    }

    [Fact]
    public async Task When_CallRegistrationsControllerActionUpdate_ItShould_ReturnUpdatedRegistrationDetails()
    {
        var request = new
        {
            Url = "api/v1/registrations",
            Body = new UpdateRegistrationCommand
            {
                Id = new Guid("1b5bdce1-68e2-4d4e-b0fa-88c23cf0bbfe"),
                Content = "contentUpdated",
                Title = "registrationUpdated"
            }
        };
        var content = SerializeContent(JsonConvert.SerializeObject(request.Body));

        var response = await _client.PutAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<BaseRegistration>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Equal(request.Body.Id, actual.Id);
        Assert.Equal(request.Body.Title, actual.Title);
        Assert.Equal(request.Body.Content, actual.Content);
    }

    [Fact]
    public async Task When_CallRegistrationsControllerActionDelete_ItShould_ReturnRegistrationId()
    {
        var response = await _client.DeleteAsync("api/v1/categories/c4abd929-0cdd-4c04-afa4-3dbeb3f686d1");
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<Guid>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.Equal(new Guid("c4abd929-0cdd-4c04-afa4-3dbeb3f686d1"), actual);
    }

    [Fact]
    public async Task When_CallRegistrationsControllerActionGetAll_ItShould_ReturnAllRegistrations()
    {
        const string request = "api/v1/registrations";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<List<DetailedRegistration>>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Contains(actual.Count, new List<int> { 4, 5, 6 });
    }

    [Fact]
    public async Task When_CallRegistrationsControllerActionGetById_ItShould_ReturnRegistrationWithSpecifiedId()
    {
        const string request = "api/v1/registrations/1b409dea-6d37-45b4-8d74-1b6c43271660";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<BaseRegistration>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Equal(new Guid("1b409dea-6d37-45b4-8d74-1b6c43271660"), actual.Id);
        Assert.Equal("registration3", actual.Title);
        Assert.Equal("content3", actual.Content);
    }

    private static StringContent SerializeContent(string content)
    {
        return new StringContent(content, Encoding.Default, "application/json");
    }
}