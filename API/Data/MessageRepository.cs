using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Pagination;
using API.Pagination.Params;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public sealed class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<PagedList<MessageDto>> GetMessagesForUserAsync(MessageParams messageParams)
        {
            //create query
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                MsgConst.MESSAGE_INBOX => query.Where(u => u.Recipient.UserName == messageParams.Username),
                MsgConst.MESSAGE_OUTBOX => query.Where(u => u.Sender.UserName == messageParams.Username),
                _ => query.Where(u => u.Recipient.UserName == messageParams.Username && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.Recipient.UserName == currentUsername
                && m.Sender.UserName == recipientUsername
                || m.Recipient.UserName == recipientUsername
                && m.Sender.UserName == currentUsername).OrderBy(m => m.MessageSent).ToListAsync();

            var unreadMesages = messages.Where(m => m.DateRead == null && m.Recipient.UserName == currentUsername).ToList();

            if(unreadMesages.Any())
            {
                unreadMesages.ForEach(msg => msg.DateRead = DateTime.Now);
                await SaveAllAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;
    }
}
