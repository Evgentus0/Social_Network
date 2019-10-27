using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SocialNetwork_DAL.Repositories
{
    public class ClientProfileManager : IClientProfileManager
    {
        private SocialNetworkContext _context;
        public ClientProfileManager(SocialNetworkContext context)
        {
            _context = context;
        }

        public void Create(ClientProfile item)
        {
            _context.ClientProfiles.Add(item);
        }

        public void Delete(string id)
        {
            var profile = _context.ClientProfiles.Find(id);
            _context.ClientProfiles.Remove(profile);
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            return _context.ClientProfiles.Include(x => x.Followers).Include(x => x.Following).Include(x => x.Publications).Include(x => x.MessageHeaders).Where(predicate);
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return _context.ClientProfiles;
        }

        public ClientProfile GetByIdWithPublications(string id)
        {
            return _context.ClientProfiles.Include(x => x.Publications).FirstOrDefault(x => x.Id == id);
        }
        public ClientProfile GetByIdWithFollowers(string id)
        {
            return _context.ClientProfiles.Include(x => x.Followers).FirstOrDefault(x => x.Id == id);
        }
        public ClientProfile GetByIdWithFollowing(string id)
        {
            return _context.ClientProfiles.Include(x => x.Following).FirstOrDefault(x => x.Id == id);
        }
        public ClientProfile GetByIdWithMessageHeaders(string id)
        {
            return _context.ClientProfiles.Include(x => x.MessageHeaders).FirstOrDefault(x => x.Id == id);
        }
        public ClientProfile GetById(string id)
        {
            return _context.ClientProfiles.Include(x => x.City).Include(x => x.Country).Include(x => x.Relationship).FirstOrDefault(x => x.Id == id);
        }

        public void Update(ClientProfile item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
