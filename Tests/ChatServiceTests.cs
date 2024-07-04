using Moq;
using Xunit;

public class ChatServiceTests
{
    [Fact]
    public async Task CreateChatAsync_ShouldCreateChat()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        var expectedChat = new Chat { Name = "Test Chat", CreatorId = 1 };
        mockRepo.Setup(repo => repo.CreateChatAsync(It.IsAny<Chat>()))
            .ReturnsAsync((Chat chat) => chat);

        // Act
        var result = await chatService.CreateChatAsync("Test Chat", 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedChat.Name, result.Name);
        Assert.Equal(expectedChat.CreatorId, result.CreatorId);
        mockRepo.Verify(repo => repo.CreateChatAsync(It.IsAny<Chat>()), Times.Once);
    }

    [Fact]
    public async Task SearchChatsAsync_ShouldReturnListOfChats()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        var searchTerm = "Test";

        var expectedChats = new List<Chat>
    {
        new Chat { Id = 1, Name = "Test Chat 1", CreatorId = 1 },
        new Chat { Id = 2, Name = "Test Chat 2", CreatorId = 2 }
    };

        mockRepo.Setup(repo => repo.SearchChatsAsync(searchTerm))
            .ReturnsAsync(expectedChats);

        // Act
        var result = await chatService.SearchChatsAsync(searchTerm);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedChats.Count, result.Count);
        Assert.Equal(expectedChats[0].Name, result[0].Name);
        Assert.Equal(expectedChats[1].Name, result[1].Name);
        mockRepo.Verify(repo => repo.SearchChatsAsync(searchTerm), Times.Once);
    }

    [Fact]
    public async Task GetChatByIdAsync_ShouldReturnChat()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        int chatId = 1;
        var expectedChat = new Chat { Id = chatId, Name = "Test Chat", CreatorId = 1 };

        mockRepo.Setup(repo => repo.GetChatByIdAsync(chatId))
            .ReturnsAsync(expectedChat);

        // Act
        var result = await chatService.GetChatByIdAsync(chatId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedChat.Id, result.Id);
        Assert.Equal(expectedChat.Name, result.Name);
        Assert.Equal(expectedChat.CreatorId, result.CreatorId);
        mockRepo.Verify(repo => repo.GetChatByIdAsync(chatId), Times.Once);
    }

    [Fact]
    public async Task DeleteChatAsync_ShouldDeleteChat()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        int chatId = 1;
        int userId = 1;

        var chatToDelete = new Chat { Id = chatId, Name = "Test Chat", CreatorId = userId };

        mockRepo.Setup(repo => repo.GetChatByIdAsync(chatId))
            .ReturnsAsync(chatToDelete);

        // Act & Assert
        await chatService.DeleteChatAsync(chatId, userId);
        mockRepo.Verify(repo => repo.DeleteChatAsync(chatId), Times.Once);
    }

    [Fact]
    public async Task DeleteChatAsync_ShouldThrowException_WhenChatNotFound()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        int chatId = 1;
        int userId = 1;

        mockRepo.Setup(repo => repo.GetChatByIdAsync(chatId))
            .ReturnsAsync((Chat)null); // Simulating chat not found

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => chatService.DeleteChatAsync(chatId, userId));
        Assert.Equal("Chat not found", ex.Message);
        mockRepo.Verify(repo => repo.DeleteChatAsync(chatId), Times.Never);
    }

    [Fact]
    public async Task DeleteChatAsync_ShouldThrowException_WhenUserHasNoPermission()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        int chatId = 1;
        int userId = 1;

        var chatToDelete = new Chat { Id = chatId, Name = "Test Chat", CreatorId = 2 }; // Different creator ID

        mockRepo.Setup(repo => repo.GetChatByIdAsync(chatId))
            .ReturnsAsync(chatToDelete);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => chatService.DeleteChatAsync(chatId, userId));
        Assert.Equal("You don't have permission to delete this chat", ex.Message);
        mockRepo.Verify(repo => repo.DeleteChatAsync(chatId), Times.Never);
    }

    [Fact]
    public async Task AddMessageAsync_ShouldAddMessageToChat()
    {
        // Arrange
        var mockRepo = new Mock<IChatRepository>();
        var chatService = new ChatService(mockRepo.Object);

        int chatId = 1;
        int userId = 1;
        string content = "Hello";

        var expectedMessage = new Message
        {
            ChatId = chatId,
            UserId = userId,
            Content = content,
            Timestamp = DateTime.UtcNow
        };

        mockRepo.Setup(repo => repo.AddMessageAsync(It.IsAny<Message>()))
            .ReturnsAsync((Message message) => message);

        // Act
        var result = await chatService.AddMessageAsync(chatId, userId, content);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMessage.ChatId, result.ChatId);
        Assert.Equal(expectedMessage.UserId, result.UserId);
        Assert.Equal(expectedMessage.Content, result.Content);  
        mockRepo.Verify(repo => repo.AddMessageAsync(It.IsAny<Message>()), Times.Once);
    }

}