using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        Task<bool> AddMessageAsync(Message message);

        void DeleteMessage(Message message);

        Task<Message> GetMessageAsync(int id);

        Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams);

        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int currentUserId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
