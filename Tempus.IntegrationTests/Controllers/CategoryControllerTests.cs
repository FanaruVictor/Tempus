using System.Text;
using System.Text.Json;
using Tempus.Core.Commands.Categories.Create;
using Tempus.Core.Commands.Categories.Update;
using Tempus.Core.Models.Category;
using Tempus.IntegrationTests.Configuration;

namespace Tempus.IntegrationTests.Controllers;

public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task When_CallCategoriesControllerActionCreate_ItShould_ReturnCreatedCategoriesDetails()
    {
        var request = new
        {
            Url = "api/categories",
            Body = new CreateCategoryCommand
            {
                UserId = new Guid("68af0be2-624d-4fe6-9a19-a83e932038bf"),
                Color = "black",
                Name = "category1"
            }
        };

        var content = SerializeContent(JsonSerializer.Serialize(request.Body));

        var response = await _client.PostAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();    

        var actual = JsonSerializer.Deserialize<BaseCategory>(responseString);

        response.EnsureSuccessStatusCode();

        Assert.NotNull(actual?.Id);
        Assert.Equal(request.Body.UserId, actual?.UserId);
        Assert.Equal(request.Body.Color, actual?.Color);
        Assert.Equal(request.Body.Name, actual?.Name);
    }

    [Fact]
    public async Task When_CallCategoriesControllerActionUpdate_ItShould_ReturnUpdatedCategoryDetails()
    {
        var request = new
        {
            Url = "api/categories",
            Body = new UpdateCategoryCommand
            {
                Id = new Guid("c8591507-e077-4ad3-a673-4d7fcb944215"),
                Color = "colorUpdated",
                Name = "categoryUpdated"
            }
        };

        var content = SerializeContent(JsonSerializer.Serialize(request.Body));

        var response = await _client.PutAsync(request.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var actual = JsonSerializer.Deserialize< BaseCategory>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Equal(request.Body.Id, actual?.Id);
        Assert.Equal(request.Body.Color, actual?.Color);
        Assert.Equal(request.Body.Name, actual?.Name);
        Assert.Equal(new Guid("68af0be2-624d-4fe6-9a19-a83e932038bf"), actual?.UserId);
    }

    [Fact]
    public async Task When_CallCategoriesControllerActionDelete_ItShould_ReturnDeletedCategoryId()
    {
        var response = await _client.DeleteAsync("api/categories/c4abd929-0cdd-4c04-afa4-3dbeb3f686d1");
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonSerializer.Deserialize<Guid>(responseString ?? "");

		response.EnsureSuccessStatusCode();
        Assert.Equal(new Guid("c4abd929-0cdd-4c04-afa4-3dbeb3f686d1"), actual);
    }

    // [Fact (Skip = "specific reason")
    [Fact]
    public async Task When_CallCategoriesControllerActionGetAll_ItShould_ReturnAllCategories()
    {
        const string request = "api/categories";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonSerializer.Deserialize<List<BaseCategory>>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Equal(5, actual?.Count);
    }

    [Fact]
    public async Task When_CallCategoriesControllerActionGetById_ItShould_ReturnCategoryWithSpecifiedId()
    {
        const string request = "api/categories/d2bbbffc-d7d0-4477-be87-d2e68aeb0ffa";

        var response = await _client.GetAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();

        var actual = JsonSerializer.Deserialize<BaseCategory>(responseString);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(actual);
        Assert.Equal(new Guid("d2bbbffc-d7d0-4477-be87-d2e68aeb0ffa"), actual?.Id);
        Assert.Equal("category5", actual?.Name);
        Assert.Equal("color5", actual?.Color);
        Assert.Equal(new Guid("6a0cd002-0cc5-4565-9852-01e3a90f01e9"), actual?.UserId);
    }

    private static StringContent SerializeContent(string content)
    {
        return new StringContent(content, Encoding.Default, "application/json");
    }
}