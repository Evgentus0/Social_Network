using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork_DAL.Entities
{
    public class MessageHeader
    {
        public int Id { get; set; }
        public string Header { get; set; }
        [ForeignKey("TypeId")]
        public MessageHeaderType Type { get; set; }
        public int? TypeId { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ClientProfile> Users { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }
        public MessageHeader()
        {
            Messages = new List<Message>();
            Users = new List<ClientProfile>();
        }
    }
}