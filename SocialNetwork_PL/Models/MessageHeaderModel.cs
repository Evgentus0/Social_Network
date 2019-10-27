using System;
using System.Collections.Generic;

namespace SocialNetwork_PL.Models
{
    public class MessageHeaderModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
        public ICollection<UserModel> Users { get; set; }
        public DateTime CreateDate { get; set; }
        public MessageHeaderTypeModel Type { get; set; }
        public bool IsRead { get; set; }
        public MessageHeaderModel()
        {
            Messages = new List<MessageModel>();
            Users = new List<UserModel>();
        }
    }
}