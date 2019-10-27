using System;
using System.Collections.Generic;

namespace SocialNetwork_DAL.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public ClientProfile Author { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }
        public ICollection<ClientProfile> UsersWhoLike { get; set; }
        public Publication()
        {
            UsersWhoLike = new List<ClientProfile>(); 
        }
    }
}