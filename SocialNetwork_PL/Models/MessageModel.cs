using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork_PL.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public MessageHeaderModel MessageHeader { get; set; }
        public UserModel Sender { get; set; }
        public ICollection<UserModel> DeleteFor { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeSend { get; set; }
        public bool IsRead { get; set; }
        public MessageModel()
        {
            DeleteFor = new List<UserModel>();
        }
    }
}