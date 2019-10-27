using SocialNetwork_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IPublicationService
    {
        void Create(PublicationDTO publication);
        void Edit(PublicationDTO publication);
        void Delete(int id);
        ICollection<PublicationDTO> GetPublicationsByUserIdHome(string userId);
        ICollection<PublicationDTO> GetPublicationsByUserIdMain(string userId);
        PublicationDTO GetById(int id);
        void LikePublication(PublicationUserLikeDTO likeDTO);
        void Dispose();
        ICollection<UserDTO> GetUsersWhoLike(int id);
    }
}
