using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.DTO
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class PublicationUserLikeDTO
    {
        public string Email { get; set; }
        public int PublicationId { get; set; }
    }

    public class MessageHeaderChangeHeaderDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
    }
    public class MessageHeaderAddMemberDTO
    {
        public string UserId { get; set; }
        public int MessageHeaderId { get; set; }
    }

}
