using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        [ForeignKey("MessageHeaderId")]
        public MessageHeader MessageHeader { get; set; }
        public int? MessageHeaderId { get; set; }
        [ForeignKey("SenderId")]
        public ClientProfile Sender { get; set; }
        public string SenderId { get; set; }
        public ICollection<ClientProfile> DeleteFor { get; set; }
        public string Content { get; set; }
        public DateTime DateTimeSend { get; set; }
        public bool IsRead { get; set; }
        public Message()
        {
            DeleteFor = new List<ClientProfile>();
        }
    }
}
