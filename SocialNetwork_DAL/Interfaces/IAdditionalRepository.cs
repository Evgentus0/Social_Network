using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IAdditionalRepository
    {
        IEnumerable<Country> GetCountry();
        IEnumerable<City> GetCity();
        IEnumerable<MessageHeaderType> GetMessageHeaderTypes();
        IEnumerable<Relationship> GetRelationship();
    }
}
