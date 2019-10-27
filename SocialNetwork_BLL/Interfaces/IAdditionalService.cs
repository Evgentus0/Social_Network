using SocialNetwork_BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Interfaces
{
    public interface IAdditionalService
    {
        ICollection<CountryDTO> GetCountry();
        ICollection<CityDTO> GetCity();
        ICollection<MessageHeaderTypeDTO> GetMessageHeaderTypes();
        ICollection<RelationshipDTO> GetRelationship();
    }
}
