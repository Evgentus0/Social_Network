using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SocialNetwork_DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private SocialNetworkContext _context;
        public MessageRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void Create(Message item)
        {
            _context.Messages.Add(item);
        }

        public void Delete(int id)
        {
            var message = _context.Messages.Find(id);
            _context.Messages.Remove(message);
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return _context.Messages.Where(predicate);
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Messages;
        }

        public Message GetById(int id)
        {
            return _context.Messages.Include(x => x.DeleteFor).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Message item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
