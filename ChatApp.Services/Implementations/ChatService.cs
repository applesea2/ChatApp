using ChatApp.DAL;
using ChatApp.Services.Interfaces;
using ChatApp.Services.Mappers;
using ChatApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesForUserAsync(string userId)
        {
            var messages = await _chatRepository.GetMessagesForUserAsync(userId);
            return messages.Select(MessageMapper.ToDTO);
        }

        public async Task SendMessageAsync(MessageDTO message)
        {
            var entity = MessageMapper.ToEntity(message);
            await _chatRepository.AddMessage(entity);
        }


    }
}
