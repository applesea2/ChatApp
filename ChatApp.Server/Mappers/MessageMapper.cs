using ChatApp.Services.Models;
using System.Reflection.Metadata.Ecma335;

namespace ChatApp.Server.Mappers
{
    public static class MessageMapper
    {
        public static MessageDTO ToDTO(MessageDTO entity)
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

        public static MessageDTO ToEntity(MessageDTO dto)
        {
            return new MessageDTO
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
