using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_PL.Infrastructure;
using SocialNetwork_PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialNetwork_PL.Filters;

namespace SocialNetwork_PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] MessageModel model)
        {
            var message = new MessageDTO
            {
                Content = model.Content,
                DateTimeSend = DateTime.Now,
                IsRead = false,
                MessageHeader = new MessageHeaderDTO { Id = model.MessageHeader.Id },
                Sender = new UserDTO { Email = User.Identity.Name }
            };
            _messageService.Create(message);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/{userId}")]
        public IHttpActionResult DeleteForOne(int id, string userId)
        {
                _messageService.DeleteForOne(id, userId);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
                _messageService.Delete(id);

            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Edit([FromBody] MessageModel model)
        {
            var message = new MessageDTO
            {
                Id = model.Id,
                Content = model.Content
            };
            _messageService.Edit(message);
            return Ok();
        }
        [HttpGet]
        [Route("header/{id}")]
        public IHttpActionResult GetByHeaderId(int id)
        {
            var messages = _messageService.GetByHeaderId(id);
            var model = CustomMapperPL.FromMessageDtoToMessageModel(messages);
            return Ok(model);
        }
    }
}
