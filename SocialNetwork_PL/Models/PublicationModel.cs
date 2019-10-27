using System;
using System.Collections.Generic;

namespace SocialNetwork_PL.Models
{
    public class PublicationModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public UserModel Author { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }
        public ICollection<UserModel> UsersWhoLike { get; set; }
        public PublicationModel()
        {
            UsersWhoLike = new List<UserModel>();
        }
    }
}