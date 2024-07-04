using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task JoinChat(int chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveChat(int chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task SendMessage(int chatId, int userId, string message)
    {
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userId, message);
    }
}