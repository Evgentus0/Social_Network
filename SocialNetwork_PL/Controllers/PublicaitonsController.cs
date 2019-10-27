using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_PL.Infrastructure;
using SocialNetwork_PL.Models;
using System;
using System.Web.Http;

namespace SocialNetwork_PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/publications")]
    public class PublicaitonsController : ApiController
    {
        private IPublicationService _publicationService;
        public PublicaitonsController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpGet]
        [Route("home/{id}")]
        public IHttpActionResult GetPublicationUserHome(string id)
        {
            var publicationModels = CustomMapperPL.FromPublicationDtoToPublicationModel(_publicationService.GetPublicationsByUserIdHome(id));
            return Ok(publicationModels);
        }

        [HttpGet]
        [Route("main/{id}")]
        public IHttpActionResult GetPublicationUserMain(string id)
        {
            var publications = CustomMapperPL.FromPublicationDtoToPublicationModel(_publicationService.GetPublicationsByUserIdMain(id));
            
            return Ok(publications);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreatePublication([FromBody]PublicationModel publication)
        {
            if (ModelState.IsValid)
            {
                var publicationDTO = new PublicationDTO
                {
                    Header = publication.Header,
                    Author = new UserDTO { Email = User.Identity.Name },
                    Content = publication.Content,
                    UsersWhoLike = null,
                    DateOfCreate = DateTime.Now
                };
                _publicationService.Create(publicationDTO);
                return Ok("Publication was creat successful");
            }
            else
            {
                return BadRequest("Incorrect input data!");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletePublication(int id)
        {
            _publicationService.Delete(id);
            return Ok("Publicaiton was delete");
        }

        [HttpPut]
        [Route("")]
        public  IHttpActionResult EditPublication([FromBody]PublicationModel publication)
        {
            if (ModelState.IsValid)
            {
                var publicationDTO = new PublicationDTO
                {
                    Header = publication.Header,
                    Author = new UserDTO { Email = User.Identity.Name },
                    Content = publication.Content,
                    UsersWhoLike = null,
                    Id=publication.Id
                };
                _publicationService.Edit(publicationDTO);
                return Ok("Publication was edite");
            }
            return BadRequest(" model is not valid");
        }

        [HttpPut]
        [Route("like")]
        public IHttpActionResult LikePublication([FromBody]PublicationUserLikeModel model)
        {
            var like = new PublicationUserLikeDTO
            {
                Email = User.Identity.Name,
                PublicationId = model.PublicationId
            };
            _publicationService.LikePublication(like);
            return Ok();
        }

        [HttpGet]
        [Route("userswholike/{id}")]
        public IHttpActionResult GetUsersWhoLike(int id)
        {
            var users = _publicationService.GetUsersWhoLike(id);
            var model = CustomMapperPL.FromUserDtoToUserModel(users);
            return Ok(model);
        }
    }
}
