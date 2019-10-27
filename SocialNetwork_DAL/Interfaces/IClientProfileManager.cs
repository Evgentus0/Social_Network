using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IClientProfileManager:IDisposable
    {
        void Create(ClientProfile item);
        ClientProfile GetByIdWithPublications(string id);
        ClientProfile GetByIdWithFollowers(string id);
        ClientProfile GetByIdWithFollowing(string id);
        ClientProfile GetByIdWithMessageHeaders(string id);
        ClientProfile GetById(string id);
        IEnumerable<ClientProfile> GetAll();
        void Update(ClientProfile item);
        void Delete(string id);
        IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate);
    }
}   
