using ChatApp.DAL;
using ChatApp.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Server
{
    public class ChatHub : Hub
    {
        private readonly IChatRepository _chatRepository;
        public async Task SendPrivateMessage(string userId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = Context.UserIdentifier,
                ReceiverId = userId,
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            await _chatRepository.AddMessage(chatMessage);
            await _chatRepository.SaveChangesAsync();

            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public async Task SendBroadCastMessage(string message)
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
