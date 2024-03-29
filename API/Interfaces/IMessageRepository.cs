﻿using API.DTOs;
using API.Entities;
using API.Pagination;
using API.Pagination.Params;

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

        /// <summary>
        /// Returns thread (chat view) of messages for 2 users
        /// </summary>
        /// <param name="currentUsername"></param>
        /// <param name="recipientUsername"></param>
        /// <returns></returns>
        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(string currentUsername, string recipientUsername);

        Task<bool> SaveAllAsync();
    }
}
