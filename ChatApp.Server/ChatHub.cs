using ChatApp.Services.Interfaces;
using ChatApp.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _messageService;

        public ChatHub(IChatService chatService)
        {
            _messageService = chatService;
        }

        public async Task SendPrivateMessage(string userId, string message)
        {
            var chatMessage = new MessageDTO
            {
                SenderId = Context.UserIdentifier,
                ReceiverId = userId,
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            await _messageService.SendMessageAsync(chatMessage);
            await Clients.Caller.SendAsync("RecieveMessage", message);
            await Clients.User(userId).SendAsync("MessageSent", message);
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public async Task SendBroadcastMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
