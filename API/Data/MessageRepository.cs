using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);

            return true;
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessageAsync(int id) => await _context.Messages.FindAsync(id);

        public Task<PagedList<MessageDto>> GetMessagesForUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int currentUserId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;
    }
}
