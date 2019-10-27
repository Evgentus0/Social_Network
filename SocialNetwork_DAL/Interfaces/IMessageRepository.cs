using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IMessageRepository
    {
        void Create(Message item);
        Message GetById(int id);
        IEnumerable<Message> GetAll();
        void Update(Message item);
        void Delete(int id);
        IEnumerable<Message> Find(Func<Message, bool> predicate);
    }
}
