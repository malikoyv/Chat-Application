public interface IChatRepository
{
    Task<Chat> CreateChatAsync(Chat chat);
    Task<List<Chat>> SearchChatsAsync(string searchTerm);
    Task<Chat> GetChatByIdAsync(int chatId);
    Task DeleteChatAsync(int chatId);
    Task<Message> AddMessageAsync(Message message);
}