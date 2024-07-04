public interface IChatService
{
    Task<Chat> CreateChatAsync(string name, int creatorId);
    Task<List<Chat>> SearchChatsAsync(string searchTerm);
    Task<Chat> GetChatByIdAsync(int chatId);
    Task DeleteChatAsync(int chatId, int userId);
    Task<Message> AddMessageAsync(int chatId, int userId, string content);
}