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
    public class MessageService:IMessageService
    {
        IUnitOfWork _dataBase;
        public MessageService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }

        public void Create(MessageDTO message)
        {
            var user = _dataBase.UserManager.FindByEmailAsync(message.Sender.Email).Result;
            var messageResult = new Message
            {
                Content = message.Content,
                DateTimeSend = message.DateTimeSend,
                IsRead = message.IsRead,
                MessageHeaderId=message.MessageHeader.Id,
                DeleteFor=null,
                SenderId=user.Id,
            };
            _dataBase.Messages.Create(messageResult);
            _dataBase.Save();
        }

        public void Delete(int id)
        {
            _dataBase.Messages.Delete(id);
            _dataBase.Save();
        }

        //show to 0- show  for 1 user; 1-show for secont user
        public void DeleteForOne(int messageId, string userId)
        {
            var message = _dataBase.Messages.GetById(messageId);
            message.DeleteFor.Add(_dataBase.Profiles.GetById(userId));
            
            _dataBase.Messages.Update(message);
            _dataBase.Save();

        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }

        public void Edit(MessageDTO message)
        {
            var messageResult = _dataBase.Messages.GetById(message.Id);
            messageResult.Content = message.Content;
            _dataBase.Messages.Update(messageResult);
            _dataBase.Save();   
        }

        public ICollection<MessageDTO> GetByHeaderId(int headerId)
        {
            var messageSource = _dataBase.MessageHeaders.GetById(headerId).Messages;
            return CustomMapperBLL.FromMessageToMessageDTO(messageSource).OrderByDescending(x => x.DateTimeSend).ToList();
        }
    }
}
