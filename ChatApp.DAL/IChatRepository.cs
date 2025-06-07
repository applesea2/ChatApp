using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.DAL
{
    public interface IChatRepository
    {
        Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(string userId);
        Task AddMessage(ChatMessage message);
    }
}
