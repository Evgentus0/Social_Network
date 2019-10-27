using SocialNetwork_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IMessageService
    {
        void Create(MessageDTO message);
        void Edit(MessageDTO message);
        void DeleteForOne(int messageId, string userId);
        void Delete(int id);
        ICollection<MessageDTO> GetByHeaderId(int headerId);
        void Dispose();
    }
}
