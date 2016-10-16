using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Salvis.DataLayer.Repositories;
using Salvis.Entities;

namespace Salvis.Framework.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            if (messageRepository == null) throw new ArgumentNullException("messageRepository");
            _messageRepository = messageRepository;
        }

        public void Dispose()
        {
            _messageRepository.Dispose();
        }

        public Message Get(int id)
        {
            return _messageRepository.Get(id);
        }

        public Message Get(long id)
        {
            return _messageRepository.Get(id);
        }

        public IEnumerable<Message> Get()
        {
            return _messageRepository.Get();
        }

        public Message Add(Message item)
        {
            return _messageRepository.Add(item);
        }

        public void Delete(Message item)
        {
            _messageRepository.Delete(item);
        }

        public void Update(Message item)
        {
            _messageRepository.Update(item);
        }

        public IEnumerable<Message> GetUnreadByUserId(string userId)
        {
            return _messageRepository.GetByUserId(userId, MessageState.Unread);
        }

        public IEnumerable<Message> GetByUserId(string userId, MessageState state)
        {
            return _messageRepository.GetByUserId(userId, state);
        }

        public IEnumerable<Message> GetByUserId(string userId)
        {
            return _messageRepository.GetByUserId(userId);
        }

        public Task MarkAsReadAsync(Int64 id)
        {
            /*
             return Task.Run(() =>
                {
                    var message = Get(id);
                    if (message != null)
                    {
                        message.State = (int)MessageState.Read;
                        _messageRepository.Update(message);
                    }
                });
             */
            return _messageRepository.MarkAsReadAsync(id);
        }

    }
}