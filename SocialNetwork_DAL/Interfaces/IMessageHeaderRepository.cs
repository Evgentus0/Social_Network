using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IMessageHeaderRepository
    {
        void Create(MessageHeader item);
        MessageHeader GetById(int id);
        IEnumerable<MessageHeader> GetAll();
        void Update(MessageHeader item);
        void Delete(int id);
        IEnumerable<MessageHeader> Find(Func<MessageHeader, bool> predicate);
    }
}
