public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Chat> CreateChatAsync(string name, int creatorId)
    {
        var chat = new Chat { Name = name, CreatorId = creatorId };
        return await _chatRepository.CreateChatAsync(chat);
    }

    public async Task<List<Chat>> SearchChatsAsync(string searchTerm)
    {
        return await _chatRepository.SearchChatsAsync(searchTerm);
    }

    public async Task<Chat> GetChatByIdAsync(int chatId)
    {
        return await _chatRepository.GetChatByIdAsync(chatId);
    }

    public async Task DeleteChatAsync(int chatId, int userId)
    {
        var chat = await _chatRepository.GetChatByIdAsync(chatId);
        if (chat == null)
        {
            throw new Exception("Chat not found");
        }
        if (chat.CreatorId != userId)
        {
            throw new Exception("You don't have permission to delete this chat");
        }
        await _chatRepository.DeleteChatAsync(chatId);
    }

    public async Task<Message> AddMessageAsync(int chatId, int userId, string content)
    {
        var message = new Message
        {
            ChatId = chatId,
            UserId = userId,
            Content = content,
            Timestamp = DateTime.UtcNow
        };
        return await _chatRepository.AddMessageAsync(message);
    }
}