using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Claims.Tests;

public class ClaimsControllerTests
{
    private readonly HttpClient _client;
    public ClaimsControllerTests()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => { });

        _client = application.CreateClient();
    }

    [Fact]
    public async Task Get_Claims()
    {
        var response = await _client.GetAsync("/Claims");

        response.EnsureSuccessStatusCode();

        // Apart from ensuring 200 OK being returned, also assert that we get a result and of correct type
        var jsonString = await response.Content.ReadAsStringAsync();
        var claims = JsonSerializer.Deserialize<IEnumerable<Claim>>(jsonString);
        Assert.IsAssignableFrom<IEnumerable<Claim>>(claims);
    }

    [Fact]
    public async Task Get_Claim()
    {
        var response = await _client.GetAsync("/Claims/id");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Add_Claim()
    {
        var badClaim = new Claim
        {
            Id = "test",
            CoverId = "bad-id",
        };
        var content = JsonContent.Create(badClaim);
        var response = await _client.PostAsync("/Claims", content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Claim()
    {
        var response = await _client.DeleteAsync("/Claims/id");
        Assert.False(response.IsSuccessStatusCode);
    }
}
