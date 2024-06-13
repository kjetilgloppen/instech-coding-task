using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Claims.Tests;

public class CoversControllerTests
{
    private readonly HttpClient _client;

    public CoversControllerTests()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { });

        _client = application.CreateClient();
    }

    [Fact]
    public async Task Get_Covers()
    {
        var response = await _client.GetAsync("/Covers");

        response.EnsureSuccessStatusCode();

        // Apart from ensuring 200 OK being returned, also assert that we get a result and of correct type
        var jsonString = await response.Content.ReadAsStringAsync();
        var claims = JsonSerializer.Deserialize<IEnumerable<Cover>>(jsonString);
        Assert.IsAssignableFrom<IEnumerable<Cover>>(claims);
    }

    [Fact]
    public async Task Get_Cover()
    {
        var response = await _client.GetAsync("/Covers/id");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Add_Cover()
    {
        var badCover = new Cover
        {
            Id = "test",
            StartDate = DateTime.UtcNow.AddDays(-1),
        };
        var content = JsonContent.Create(badCover);
        var response = await _client.PostAsync("/Covers", content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Cover()
    {
        var response = await _client.DeleteAsync("/Cover/id");
        Assert.False(response.IsSuccessStatusCode);
    }
}
