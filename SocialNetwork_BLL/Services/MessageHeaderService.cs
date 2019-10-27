using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork_BLL.Infrastructure;

namespace SocialNetwork_BLL.Services
{
    public class MessageHeaderService : IMessageHeaderService
    {
        IUnitOfWork _dataBase;
        public MessageHeaderService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }


        public void ChangeHeader(MessageHeaderChangeHeaderDTO messageHeader)
        {
            var header = _dataBase.MessageHeaders.GetById(messageHeader.Id);
            header.Header = messageHeader.Header;
            _dataBase.MessageHeaders.Update(header);
            _dataBase.Save();
        }

        public void AddMember(MessageHeaderAddMemberDTO addMember)
        {
            var header = _dataBase.MessageHeaders.GetById(addMember.MessageHeaderId);
            header.Users.Add(_dataBase.Profiles.GetById(addMember.UserId));
            _dataBase.MessageHeaders.Update(header);
            _dataBase.Save();
        }

        public void Create(MessageHeaderDTO messageHeader)
        {
            ICollection<ClientProfile> users = new List<ClientProfile>();
            foreach(var u in messageHeader.Users)
            {
                users.Add(_dataBase.Profiles.GetById(u.Id));
            }
            var message = new MessageHeader
            {
                CreateDate = messageHeader.CreateDate,
                IsRead = messageHeader.IsRead,
                Users=users,
                Messages = null,
                Header=messageHeader.Header,
                TypeId=messageHeader.Type.Id
            };
            _dataBase.MessageHeaders.Create(message);
            _dataBase.Save();
        }

        public void Delete(int id)
        {
            _dataBase.MessageHeaders.Delete(id);
            _dataBase.Save();
        }

        public void DeleteForOne(int id, string email)
        {
            var header = _dataBase.MessageHeaders.GetById(id);
            var app = _dataBase.UserManager.FindByEmailAsync(email).Result;
            header.Users.Remove(_dataBase.Profiles.GetById(app.Id));
            _dataBase.MessageHeaders.Update(header);
            _dataBase.Save();
        }

        public MessageHeaderDTO GetById(int id)
        {
            var messageHeader = _dataBase.MessageHeaders.GetById(id);
            return CustomMapperBLL.FromMessageHeaderToMessageHeaderDTO(messageHeader, false);
        }
        //from message to messageDto check after
        public ICollection<MessageHeaderDTO> GetMessageHeadersByUserEmail(string email)
        {
            var userId = _dataBase.UserManager.FindByEmailAsync(email).Result.Id;

            var messageHeaders = _dataBase.Profiles.GetByIdWithMessageHeaders(userId).MessageHeaders;
            var messageHeaderDTO = new List<MessageHeaderDTO>();
            foreach(var m in messageHeaders)
            {
                messageHeaderDTO.Add(CustomMapperBLL.FromMessageHeaderToMessageHeaderDTO(_dataBase.MessageHeaders.GetById(m.Id), false));
            }
            return messageHeaderDTO;

        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}
