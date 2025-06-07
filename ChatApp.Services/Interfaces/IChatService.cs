using ChatApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<MessageDTO>> GetMessagesForUserAsync(string userId);
        Task SendMessageAsync(MessageDTO message);
    }
}
