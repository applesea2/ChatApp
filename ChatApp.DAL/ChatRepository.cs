using ChatApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DAL
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(string userId)
        {
            return await _context.ChatMessages
                .Where(m => m.ReceiverId == userId || m.SenderId == userId)
                .ToListAsync();
        }

        public async Task AddMessage(ChatMessage message)
        {
            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}