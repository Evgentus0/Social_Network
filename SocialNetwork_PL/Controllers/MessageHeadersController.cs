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

namespace SocialNetwork_PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/messageheaders")]
    public class MessageHeadersController : ApiController
    {
        private IMessageHeaderService _messageHeaderService;
        public MessageHeadersController(IMessageHeaderService messageHeaderService)
        {
            _messageHeaderService = messageHeaderService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetMessageHeadersUser()
        {
            var headers = _messageHeaderService.GetMessageHeadersByUserEmail(User.Identity.Name);
            var model = CustomMapperPL.FromMessageHeaderDtoToMessageHeaderModel(headers);
            return Ok(model);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetMessageHeader(int id)
        {
            var header = CustomMapperPL.FromMessageHeaderDtoToMessageHeaderModel(_messageHeaderService.GetById(id), false);
            return Ok(header);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateMessageHeader([FromBody] MessageHeaderModel model)
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (var u in model.Users)
            {
                users.Add(new UserDTO { Id = u.Id });
            }

            var header = new MessageHeaderDTO
            {
                CreateDate = DateTime.Now,
                Header = model.Header,
                IsRead = false,
                Type = new MessageHeaderTypeDTO { Id = model.Type.Id },
                Users = users

            };
            _messageHeaderService.Create(header);
            return Ok();
        }
        [HttpDelete]
        [Route("forone/{id}")]
        public IHttpActionResult DeleteMessageHeaderForOne(int id)
        {
            _messageHeaderService.DeleteForOne(id, User.Identity.Name);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteMessageHeader(int id)
        {
            _messageHeaderService.Delete(id);
            return Ok();
        }


        [HttpPut]
        [Route("changeheader")]
        public IHttpActionResult ChangeHeader([FromBody]ChangeHeaderModel change)
        {
            var header = new MessageHeaderChangeHeaderDTO
            {
                Id = change.Id,
                Header = change.Header
            };
            _messageHeaderService.ChangeHeader(header);
            return Ok();
        }
        [HttpPut]
        [Route("addmember")]
        public IHttpActionResult AddMember([FromBody] AddMemberModel add)
        {
            var addMember = new MessageHeaderAddMemberDTO
            {
                MessageHeaderId = add.MessageHeaderId,
                UserId = add.UserId
            };
            _messageHeaderService.AddMember(addMember);
            return Ok();
        }
    }
}
