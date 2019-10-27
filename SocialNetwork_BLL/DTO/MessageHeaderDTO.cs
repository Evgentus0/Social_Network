using System;
using System.Collections.Generic;

namespace SocialNetwork_BLL.DTO
{
    public class MessageHeaderDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public ICollection<MessageDTO> Messages { get; set; }
        public MessageHeaderTypeDTO Type { get; set; }
        public ICollection<UserDTO> Users { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }
        public MessageHeaderDTO()
        {
            Messages = new List<MessageDTO>();
        }
    }
}