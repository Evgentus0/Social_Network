using System;
using System.Collections.Generic;

namespace SocialNetwork_BLL.DTO
{
    public class PublicationDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public UserDTO Author { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }
        public ICollection<UserDTO> UsersWhoLike { get; set; }
        public PublicationDTO()
        {
            UsersWhoLike = new List<UserDTO>();
        }
    }
}