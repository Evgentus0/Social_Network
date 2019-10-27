using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNetwork_DAL.Repositories
{
    public class MessageHeaderRepository : IMessageHeaderRepository
    {
        private SocialNetworkContext _context;
        public MessageHeaderRepository(SocialNetworkContext context)
        {
            _context = context;
        }
        public void Create(MessageHeader item)
        {
            _context.MessageHeaders.Add(item);
        }

        public void Delete(int id)
        {
            var messageHeader = _context.MessageHeaders.Find(id);
            _context.MessageHeaders.Remove(messageHeader);
        }

        public IEnumerable<MessageHeader> Find(Func<MessageHeader, bool> predicate)
        {
            return _context.MessageHeaders.Include(x => x.Messages).Where(predicate);
        }

        public IEnumerable<MessageHeader> GetAll()
        {
            return _context.MessageHeaders.Include(x => x.Messages);
        }

        public MessageHeader GetById(int id)
        {
            return _context.MessageHeaders.Include(x => x.Messages).Include(x => x.Users).Include(x=>x.Type).FirstOrDefault(x => x.Id == id);
        }

        public void Update(MessageHeader item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
