using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SocialNetwork_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(UserDTO user);
        Task<ClaimsIdentity> AuthenticateAsync(UserLoginDTO user);
        void BlockUser(string id);
        UserDTO GetUserByEmail(string email);
        UserDTO GetUserById(string id);
        void Subscribe(string email, string id);
        void UnSubscribe(string email, string id);
        ICollection<UserDTO> GetFollowersById(string id);
        ICollection<UserDTO> GetFollowingById(string id);
        ICollection<UserDTO> GetAll();
        IdentityResult Delete(string id);
        void Dispose();
    }
}
