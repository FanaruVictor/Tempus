using System.Text;
using Newtonsoft.Json;
using Tempus.Core.Commands.Users.Create;
using Tempus.Core.Commands.Users.Update;
using Tempus.Core.Models.User;
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
    public async Task When_CallUsersControllerActionCreate_ItShould_ReturnCreatedUserDetails()
    {
        var request = new
        {
            Url = "api/users",
            Body = new CreateUserCommand
            {
                UserName = "victor",
                Email = "victor.fanaru"
            }
        };

        var content = SerializeContent(JsonConvert.SerializeObject(request.Body));

        var response = await _client.PostAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<BaseUser>(responseString);

        response.EnsureSuccessStatusCode();

        Assert.NotNull(actual?.Id);
        Assert.Equal(request.Body.UserName, actual?.UserName);
        Assert.Equal(request.Body.Email, actual?.Email);
    }

    [Fact]
    public async Task When_CallUsersControllerActionUpdate_ItShould_ReturnUpdatedUserDetails()
    {
        var request = new
        {
            Url = "api/users",

            Body = new UpdateUserCommand
            {
                Id = new Guid("68cf3d1c-07c9-4df4-898e-30db1ebeb888"),
                UserName = "mihai",
                Email = "mihai@fanaru"
            }
        };


        var content = SerializeContent(JsonConvert.SerializeObject(request.Body));

        var response = await _client.PutAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<BaseUser>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Equal(request.Body.Id, actual?.Id);
        Assert.Equal(request.Body.UserName, actual?.UserName);
        Assert.Equal(request.Body.Email, actual?.Email);
    }

    [Fact]
    public async Task When_CallUsersControllerActionDelete_ItShould_ReturnDeletedUserId()
    {
        var response = await _client.DeleteAsync("api/users/6627df4f-6ac6-4ff6-bf8e-6d358fd88025");
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<Guid>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.Equal(new Guid("6627df4f-6ac6-4ff6-bf8e-6d358fd88025"), actual);
    }

    // [Fact (Skip = "specific reason")]
    [Fact]
    public async Task When_CallUsersControllerActionGetAll_ItShould_ReturnAllUsers()
    {
        const string request = "api/users";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<List<BaseUser>>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Contains(actual.Count, new List<int> { 4, 5, 6 });
    }

    [Fact]
    public async Task When_CallUsersControllerActionGetById_ItShould_ReturnUserWithSpecifiedId()
    {
        const string request = "api/users/68cf3d1c-07c9-4df4-898e-30db1ebeb888";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonConvert.DeserializeObject<BaseUser>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Equal(new Guid("68cf3d1c-07c9-4df4-898e-30db1ebeb888"), actual?.Id);
        Assert.Equal("victor", actual?.UserName);
        Assert.Equal("victor@daniel", actual?.Email);
    }

    private static StringContent SerializeContent(string content)
    {
        return new StringContent(content, Encoding.Default, "application/json");
    }
}