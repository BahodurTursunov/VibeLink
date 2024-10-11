using Microsoft.AspNetCore.SignalR;
using VibeLink_Server.Hubs.Interface;
using VibeLink_Server.Models;

namespace VibeLink_Server.Hubs.Implementation
{
    public class ChatHub : Hub<IChatClient>
    {

        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage("Admin", $"{connection.UserName} присоединился к чату");
        }

        //public Task SendMessage(string message)
        //{
        //    return Clients.Others.SendAsync("Send", message);
        //}
    }
}
