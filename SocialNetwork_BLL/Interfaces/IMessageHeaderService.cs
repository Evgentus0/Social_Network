using SocialNetwork_BLL.DTO;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IMessageHeaderService
    {
        void Create(MessageHeaderDTO messageHeader);
        void Delete(int id);
        void DeleteForOne(int id, string userId);
        ICollection<MessageHeaderDTO> GetMessageHeadersByUserEmail(string email);
        MessageHeaderDTO GetById(int id);
        void ChangeHeader(MessageHeaderChangeHeaderDTO messageHeader);
        void AddMember(MessageHeaderAddMemberDTO addMember);
        void Dispose();

    }
}
