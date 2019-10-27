using System;
using System.Collections.Generic;



namespace SocialNetwork_BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public MessageHeaderDTO MessageHeader { get; set; }
        public UserDTO Sender { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeSend { get; set; }
        public ICollection<UserDTO> DeleteFor { get; set; }
        public bool IsRead { get; set; }
        public MessageDTO()
        {
            DeleteFor = new List<UserDTO>();
        }
    }
}