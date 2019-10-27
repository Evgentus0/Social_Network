using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Infrastructure;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_BLL.Services
{
    public class AdditionalService:IAdditionalService
    {
        IUnitOfWork _dataBase;
        public AdditionalService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }

        public ICollection<CityDTO> GetCity()
        {
            return CustomMapperBLL.FromCityToCityDTO(_dataBase.Additionals.GetCity());
        }

        public ICollection<CountryDTO> GetCountry()
        {
            return CustomMapperBLL.FromCountryToCountryDTO(_dataBase.Additionals.GetCountry());
        }

        public ICollection<MessageHeaderTypeDTO> GetMessageHeaderTypes()
        {
            return CustomMapperBLL.FromMessageHeaderTypeToMessageHeaderTypeDTO(_dataBase.Additionals.GetMessageHeaderTypes());
        }

        public ICollection<RelationshipDTO> GetRelationship()
        {
            return CustomMapperBLL.FromRelationshipToRelationshipDTO(_dataBase.Additionals.GetRelationship());
        }
    }
}
