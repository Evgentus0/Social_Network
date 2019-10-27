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
    public class PublicationService : IPublicationService
    {
        IUnitOfWork _dataBase;

        public PublicationService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }

        public void Create(PublicationDTO publication)
        {
            var user = _dataBase.UserManager.FindByEmailAsync(publication.Author.Email).Result;
            _dataBase.Publications.Create(new Publication
            {
                Header = publication.Header,
                AuthorId = user.Id,
                Content = publication.Content,
                UsersWhoLike = null,
                DateOfCreate = publication.DateOfCreate
            });
            _dataBase.Save();
        }

        public void Delete(int id)
        {
            _dataBase.Publications.Delete(id);
            _dataBase.Save();
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }

        public void Edit(PublicationDTO publication)
        {
            var publicationResult = _dataBase.Publications.GetById(publication.Id);
            publicationResult.Header = publication.Header;
            publicationResult.Content = publication.Content;
            _dataBase.Publications.Update(publicationResult);
            _dataBase.Save();
        }

        public PublicationDTO GetById(int id)
        {
            var publication = _dataBase.Publications.GetById(id);
            return CustomMapperBLL.FromPublucationToPublicationDTO(publication, false);
        }

        public ICollection<PublicationDTO> GetPublicationsByUserIdHome(string userId)
        {
            var publications = _dataBase.Profiles.GetByIdWithPublications(userId).Publications;
            var result = new List<PublicationDTO>();
            foreach(var p in publications)
            {
                result.Add(CustomMapperBLL.FromPublucationToPublicationDTO(_dataBase.Publications.GetById(p.Id), false));
            }

            return result;

        }
        public ICollection<PublicationDTO> GetPublicationsByUserIdMain(string id)
        {
            var users = _dataBase.Profiles.GetByIdWithFollowing(id).Following;
            IEnumerable<PublicationDTO> publication = new List<PublicationDTO>();
            foreach (var f in users)
            {
                var publ = _dataBase.Profiles.GetByIdWithPublications(f.Id).Publications;
                publication = publication.Union(CustomMapperBLL.FromPublucationToPublicationDTO(publ));
            }
            return publication.OrderByDescending(x => x.DateOfCreate).ToList();
        }

        public void LikePublication(PublicationUserLikeDTO likeDTO)
        {
            var publication = _dataBase.Publications.GetById(likeDTO.PublicationId);
            var user = _dataBase.UserManager.FindByEmailAsync(likeDTO.Email).Result;
            var profile = _dataBase.Profiles.GetById(user.Id);
            if (publication.UsersWhoLike.Contains(profile))
            {
                publication.UsersWhoLike.Remove(profile);
            }
            else
            {
                publication.UsersWhoLike.Add(profile);
            }
            _dataBase.Publications.Update(publication);
            _dataBase.Save();
        }

        public ICollection<UserDTO> GetUsersWhoLike(int id)
        {
            var users = _dataBase.Publications.GetById(id).UsersWhoLike;
            return CustomMapperBLL.FromClientProfileToUserDTO(users);
        }
    }
}
