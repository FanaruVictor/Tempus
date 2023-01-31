using System.Text;
using Newtonsoft.Json;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Commands.Auth.Register;
using Tempus.Infrastructure.Commands.Users.Update;
using Tempus.Infrastructure.Models.User;
using Tempus.IntegrationTests.Configuration;

namespace Tempus.IntegrationTests.Controllers;

public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task When_CallUsersControllerActionUpdate_ItShould_ReturnUpdatedUserDetails()
    {
        var request = new
        {
            Url = "api/v1/users",

            Body = new UpdateUserCommand
            {
                Id = new Guid("68cf3d1c-07c9-4df4-898e-30db1ebeb888"),
                UserName = "mihai",
                Email = "mihai@fanaru.com"
            }
        };
        var content = SerializeContent(JsonConvert.SerializeObject(request.Body));

        var response = await _client.PutAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<BaseUser>>(responseString);
        var actual = baseResponse?.Resource;

        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Equal(request.Body.Id, actual.Id);
        Assert.Equal(request.Body.UserName, actual.UserName);
        Assert.Equal(request.Body.Email, actual.Email);
    }

    [Fact]
    public async Task When_CallUsersControllerActionDelete_ItShould_ReturnDeletedUserId()
    {
        var response = await _client.DeleteAsync("api/v1/users/6627df4f-6ac6-4ff6-bf8e-6d358fd88025");
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<Guid>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.Equal(new Guid("6627df4f-6ac6-4ff6-bf8e-6d358fd88025"), actual);
    }

    [Fact]
    public async Task When_CallUsersControllerActionGetAll_ItShould_ReturnAllUsers()
    {
        const string request = "api/v1/users";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<List<BaseUser>>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Contains(actual.Count, new List<int> { 4, 5, 6 });
    }

    [Fact]
    public async Task When_CallUsersControllerActionGetById_ItShould_ReturnUserWithSpecifiedId()
    {
        const string request = "api/v1/users/687318c1-1b11-4b1f-abda-7d230bb18ee1";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse<BaseUser>>(responseString);
        var actual = baseResponse?.Resource;
        
        response.EnsureSuccessStatusCode();
        Assert.NotNull(baseResponse);
        Assert.NotNull(actual);
        Assert.Equal(new Guid("687318c1-1b11-4b1f-abda-7d230bb18ee1"), actual.Id);
        Assert.Equal("fanaru", actual.UserName);
        Assert.Equal("victor.daniel@fanaru", actual.Email);
    }

    private static StringContent SerializeContent(string content)
    {
        return new StringContent(content, Encoding.Default, "application/json");
    }
}