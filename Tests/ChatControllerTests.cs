using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

public class ChatControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ChatControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateChat_ReturnsCreatedResponse()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new
        {
            Url = "/api/chat",
            Body = new { name = "Test Chat", creatorId = 1 }
        };

        // Act
        var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var chat = await response.Content.ReadAsStringAsync();
        Assert.Contains("Test Chat", chat); // Check if response contains created chat name
    }

    [Fact]
    public async Task SearchChats_ReturnsOkResponse()
    {
        // Arrange
        var client = _factory.CreateClient();
        var searchTerm = "Test";

        // Act
        var response = await client.GetAsync($"/api/chat/search?searchTerm={searchTerm}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var chats = await response.Content.ReadAsStringAsync();
        Assert.Contains("Test", chats); // Check if response contains chats with the search term
    }

    [Fact]
    public async Task GetChat_ReturnsOkResponse()
    {
        // Arrange
        var client = _factory.CreateClient();
        int chatId = 1;

        // Act
        var response = await client.GetAsync($"/api/chat/{chatId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var chat = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"id\":1", chat); // Check if response contains chat with the specified ID
    }

    [Fact]
    public async Task DeleteChat_ReturnsNoContentResponse()
    {
        // Arrange
        var client = _factory.CreateClient();
        int chatId = 1;
        int userId = 1;

        // Act
        var response = await client.DeleteAsync($"/api/chat/{chatId}?userId={userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task AddMessage_ReturnsCreatedResponse()
    {
        // Arrange
        var client = _factory.CreateClient();
        int chatId = 1;
        int userId = 1;
        var request = new
        {
            Url = $"/api/chat/{chatId}/messages",
            Body = new { userId = userId, content = "Hello" }
        };

        // Act
        var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var message = await response.Content.ReadAsStringAsync();
        Assert.Contains("Hello", message); // Check if response contains the added message content
    }
}

// Helper class to convert object to JSON content
public static class ContentHelper
{
    public static StringContent GetStringContent(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
