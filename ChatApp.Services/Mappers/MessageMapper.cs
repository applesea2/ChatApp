
using ChatApp.DAL.Entities;
using ChatApp.Services.Models;

namespace ChatApp.Services.Mappers
{
    public static class MessageMapper
    {
        public static MessageDTO ToDTO(ChatMessage entity)
        {
            return new MessageDTO
            {
                Id = entity.Id,
                SenderId = entity.SenderId,
                ReceiverId = entity.ReceiverId,
                Content = entity.Content,
                Timestamp = entity.Timestamp
            };
        }

        public static ChatMessage ToEntity(MessageDTO dto)
        {
            return new ChatMessage
            {
                Id = dto.Id,
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                Timestamp = dto.Timestamp
            };
        }
    }

}
