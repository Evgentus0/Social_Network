using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SocialNetwork_DAL.Repositories
{
    internal class PublicationRepository : IPublicationRepository
    {
        private SocialNetworkContext _context;
        public PublicationRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public void Create(Publication item)
        {
            _context.Publications.Add(item);
        }

        public void Delete(int id)
        {
            var publication = _context.Publications.Find(id);
            if (publication == null)
                throw new NullReferenceException("Publication isn't exist");
            _context.Publications.Remove(publication);
        }

        public IEnumerable<Publication> Find(Func<Publication, bool> predicate)
        {
            return _context.Publications.Where(predicate);
        }

        public IEnumerable<Publication> GetAll()
        {
            return _context.Publications;
        }

        public Publication GetById(int id)
        {
            return _context.Publications.Include(x => x.UsersWhoLike).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Publication item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
