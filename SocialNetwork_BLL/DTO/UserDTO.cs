using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalInfo { get; set; }
        public RelationshipDTO Relationship { get; set; }
        public CountryDTO Country { get; set; }
        public CityDTO City { get; set; }
        public ICollection<PublicationDTO> Publications { get; set; }
        public ICollection<PublicationDTO> LikedPublications { get; set; }
        public string PictureProfilePath { get; set; }
        public ICollection<UserDTO> Followers { get; set; }
        public ICollection<UserDTO> Following { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<MessageHeaderDTO> MessageHeaders { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public UserDTO()
        {
            Publications = new List<PublicationDTO>();
            Followers = new List<UserDTO>();
            Following = new List<UserDTO>();
            MessageHeaders = new List<MessageHeaderDTO>();
            LikedPublications = new List<PublicationDTO>();
        }
    }
}
