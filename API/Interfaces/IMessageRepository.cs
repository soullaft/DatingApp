using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    /// <summary>
    /// Message repository contract
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Add message async
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> AddMessageAsync(Message message);

        /// <summary>
        /// Delete message
        /// </summary>
        /// <param name="message"></param>
        void DeleteMessage(Message message);

        /// <summary>
        /// Get message by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Message> GetMessageAsync(int id);

        /// <summary>
        /// get messages for the current user
        /// </summary>
        /// <param name="messageParams"></param>
        /// <returns></returns>
        Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams);

        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int currentUserId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
