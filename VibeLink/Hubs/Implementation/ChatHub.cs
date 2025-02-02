﻿using Microsoft.AspNetCore.SignalR;
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
                .ReceiveMessage("Admin", $"{connection.UserName} присоединился к группе");
        }

        public async Task LeaveChat(UserConnection connection)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

            await Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage("Admin", $"{connection.UserName} вышел из группы");
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }

        //public Task SendMessage(string message)
        //{
        //    return Clients.Others.SendAsync("Send", message);
        //}
    }
}
