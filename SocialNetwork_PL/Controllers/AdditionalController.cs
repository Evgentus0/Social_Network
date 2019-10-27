using SocialNetwork_BLL.Interfaces;
using SocialNetwork_PL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialNetwork_PL.Controllers
{
    [RoutePrefix("api/additional")]
    public class AdditionalController : ApiController
    {
        IAdditionalService _serivce;
        public AdditionalController(IAdditionalService serivce)
        {
            _serivce = serivce;
        }

        [HttpGet]
        [Route("country")]
        public IHttpActionResult GetCountries()
        {
            var countries = CustomMapperPL.FromCountryDtoToCountryModel(_serivce.GetCountry());
            return Ok(countries);
        }

        [HttpGet]
        [Route("city")]
        public IHttpActionResult GetCities()
        {
            var cities = CustomMapperPL.FromCityDtoToCityModel(_serivce.GetCity());
            return Ok(cities);
        }

        [HttpGet]
        [Route("relationship")]
        public IHttpActionResult GetRelationship()
        {
            var relationships = CustomMapperPL.FromRelationshipDtoToRelationshipModel(_serivce.GetRelationship());
            return Ok(relationships);
        }

        [HttpGet]
        [Route("messageheadertype")]
        public IHttpActionResult GetMessageHeaderType()
        {
            var types = CustomMapperPL.FromMessageHeaderTypeDtoToMessageHeaderTypeModel(_serivce.GetMessageHeaderTypes());
            return Ok(types);
        }
    }
}
