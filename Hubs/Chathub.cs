using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class Chathub : Hub
    {
        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.
                SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined");
        }

        public async Task JoinSpecificChatroom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.Chatroom);
            await Clients.Group(conn.Chatroom).SendAsync("ReceiveMessage", "admin", $"{conn.UserName} has joined {conn.Chatroom}");
        }


    }
}
