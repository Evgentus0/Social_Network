using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SocialNetwork_DAL.Repositories
{
    public class AdditionalRepository : IAdditionalRepository
    {
        private SocialNetworkContext _context;

        public AdditionalRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCity()
        {
            return _context.Cities.Include(x => x.Country);
        }

        public IEnumerable<Country> GetCountry()
        {
            return _context.Countries;
        }

        public IEnumerable<MessageHeaderType> GetMessageHeaderTypes()
        {
            return _context.MessageHeaderTypes;
        }

        public IEnumerable<Relationship> GetRelationship()
        {
            return _context.Relationships;
        }
    }
}
