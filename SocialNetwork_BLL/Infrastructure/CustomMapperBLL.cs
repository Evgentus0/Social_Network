using AutoMapper;
using SocialNetwork_BLL.DTO;
using SocialNetwork_DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork_BLL.Infrastructure
{
    public class CustomMapperBLL
    {
        private static MapperConfiguration _fromClientProfileToUserDTO;
        private static MapperConfiguration _fromPublicationToPublicationDTO;
        private static MapperConfiguration _fromMessageToMessageDTO;
        private static MapperConfiguration _fromMessageHeaderToMessageHeaderDTO;
        private static MapperConfiguration _fromCountryToCountryDTO;
        private static MapperConfiguration _fromCityToCityDTO;
        private static MapperConfiguration _fromMessageHeaderTypeToMessageHeaderTypeDTO;
        private static MapperConfiguration _fromRelationshipToRelationshipDTO;


        static CustomMapperBLL()
        {
            _fromClientProfileToUserDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientProfile, UserDTO>()
                .ForMember(d => d.Publications, a => a.Ignore())
                .ForMember(d => d.LikedPublications, a => a.Ignore())
                .ForMember(d => d.Followers, a => a.Ignore())
                .ForMember(d => d.Following, a => a.Ignore())
                .ForMember(d => d.MessageHeaders, a => a.Ignore());
                cfg.CreateMap<City, CityDTO>();
                cfg.CreateMap<Country, CountryDTO>();
                cfg.CreateMap<ApplicationUser, UserDTO>();
                cfg.CreateMap<Relationship, RelationshipDTO>();
            }
            );

            _fromPublicationToPublicationDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Publication, PublicationDTO>()
             .ForMember(d => d.UsersWhoLike, a => a.Ignore());
                cfg.CreateMap<ClientProfile, UserDTO>()
            .ForMember(d => d.Publications, a => a.Ignore())
            .ForMember(d => d.LikedPublications, a => a.Ignore())
            .ForMember(d => d.Followers, a => a.Ignore())
            .ForMember(d => d.Following, a => a.Ignore())
            .ForMember(d => d.MessageHeaders, a => a.Ignore());

            }
            );

            _fromMessageToMessageDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Message, MessageDTO>()
            .ForMember(d => d.DeleteFor, a => a.Ignore());
                cfg.CreateMap<ClientProfile, UserDTO>()
            .ForMember(d => d.Publications, a => a.Ignore())
            .ForMember(d => d.LikedPublications, a => a.Ignore())
            .ForMember(d => d.Followers, a => a.Ignore())
            .ForMember(d => d.Following, a => a.Ignore())
            .ForMember(d => d.MessageHeaders, a => a.Ignore());
                cfg.CreateMap<MessageHeader, MessageHeaderDTO>()
            .ForMember(d => d.Users, a => a.Ignore())
            .ForMember(d => d.Messages, a => a.Ignore());
                cfg.CreateMap<MessageHeaderType, MessageHeaderTypeDTO>();
            }
            );

            _fromMessageHeaderToMessageHeaderDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageHeader, MessageHeaderDTO>()
                .ForMember(d => d.Users, a => a.Ignore())
                .ForMember(d => d.Messages, a => a.Ignore());
                cfg.CreateMap<MessageHeaderType, MessageHeaderTypeDTO>();


            }
            );
            _fromCountryToCountryDTO = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<Country, CountryDTO>();
              });
            _fromCityToCityDTO = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<City, CityDTO>();
                  cfg.CreateMap<Country, CountryDTO>();
              });
            _fromMessageHeaderTypeToMessageHeaderTypeDTO = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<MessageHeaderType, MessageHeaderTypeDTO>();
              });
            _fromRelationshipToRelationshipDTO = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<Relationship, RelationshipDTO>();
              });
        }

        public static UserDTO FromClientProfileToUserDTO(ClientProfile source, bool isRecurtion)
        {
            var user = _fromClientProfileToUserDTO.CreateMapper().Map<ClientProfile, UserDTO>(source);
            if (!isRecurtion)
            {
                int maxLength = Max(source.Publications.Count, source.LikedPublications.Count, source.Followers.Count, source.Following.Count, source.MessageHeaders.Count);
                for (int i = 0; i < maxLength; i++)
                {
                    if (i < user.Publications.Count)
                    {
                        user.Publications.Add(FromPublucationToPublicationDTO(source.Publications.ElementAt(i), true));
                    }
                    if (i < user.LikedPublications.Count)
                    {
                        user.LikedPublications.Add(FromPublucationToPublicationDTO(source.LikedPublications.ElementAt(i), true));
                    }
                    if (i < user.Followers.Count)
                    {
                        user.Followers.Add(FromClientProfileToUserDTO(source.Followers.ElementAt(i), true));
                    }
                    if (i < user.Following.Count)
                    {
                        user.Following.Add(FromClientProfileToUserDTO(source.Following.ElementAt(i), true));
                    }
                    if (i < user.MessageHeaders.Count)
                    {
                        user.MessageHeaders.Add(FromMessageHeaderToMessageHeaderDTO(source.MessageHeaders.ElementAt(i), true));
                    }
                }
            }
            return user;
        }
        public static List<UserDTO> FromClientProfileToUserDTO(IEnumerable<ClientProfile> source)
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (var s in source)
            {
                users.Add(FromClientProfileToUserDTO(s, false));
            }
            return users;
        }

        public static PublicationDTO FromPublucationToPublicationDTO(Publication source, bool isRecurtion)
        {
            var publication = _fromPublicationToPublicationDTO.CreateMapper().Map<Publication, PublicationDTO>(source);
            if (!isRecurtion)
            {
                foreach (var p in source.UsersWhoLike)
                {
                    publication.UsersWhoLike.Add(FromClientProfileToUserDTO(p, true));
                }
            }
            return publication;
        }
        public static List<PublicationDTO> FromPublucationToPublicationDTO(IEnumerable<Publication> source)
        {
            List<PublicationDTO> publications = new List<PublicationDTO>();
            foreach (var p in source)
            {
                publications.Add(FromPublucationToPublicationDTO(p, false));
            }
            return publications;
        }

        public static MessageDTO FromMessageToMessageDTO(Message source, bool isRecurtion)
        {
            var message = _fromMessageToMessageDTO.CreateMapper().Map<Message, MessageDTO>(source);
            if (!isRecurtion)
            {
                foreach (var p in source.DeleteFor)
                {
                    message.DeleteFor.Add(FromClientProfileToUserDTO(p, true));
                }
            }
            return message;
        }
        public static List<MessageDTO> FromMessageToMessageDTO(IEnumerable<Message> source)
        {
            List<MessageDTO> messages = new List<MessageDTO>();
            foreach (var m in source)
            {
                messages.Add(FromMessageToMessageDTO(m, false));
            }
            return messages;
        }

        public static MessageHeaderDTO FromMessageHeaderToMessageHeaderDTO(MessageHeader source, bool isRecurtion)
        {
            var messageHeader = _fromMessageHeaderToMessageHeaderDTO.CreateMapper().Map<MessageHeader, MessageHeaderDTO>(source);
            messageHeader.Users = new List<UserDTO>();
            if (!isRecurtion)
            {
                foreach (var u in source.Users)
                { 
                    messageHeader.Users.Add(FromClientProfileToUserDTO(u, true));
                }
                foreach (var m in source.Messages)
                {
                    messageHeader.Messages.Add(FromMessageToMessageDTO(m, true));
                }
            }
            return messageHeader;
        }
        public static List<MessageHeaderDTO> FromMessageHeaderToMessageHeaderDTO(IEnumerable<MessageHeader> source)
        {
            List<MessageHeaderDTO> messageHeaders = new List<MessageHeaderDTO>();
            foreach (var p in source)
            {
                messageHeaders.Add(FromMessageHeaderToMessageHeaderDTO(p, false));
            }
            return messageHeaders;
        }

        public static CountryDTO FromCountryToCountryDTO(Country source)
        {
            return _fromCountryToCountryDTO.CreateMapper().Map<Country, CountryDTO>(source);
        }
        public static List<CountryDTO> FromCountryToCountryDTO(IEnumerable<Country> source)
        {
            List<CountryDTO> countries = new List<CountryDTO>();
            foreach(var s in source)
            {
                countries.Add(FromCountryToCountryDTO(s));
            }
            return countries;
        }

        public static CityDTO FromCityToCityDTO(City source)
        {
            return _fromCityToCityDTO.CreateMapper().Map<City, CityDTO>(source);
        }
        public static List<CityDTO> FromCityToCityDTO(IEnumerable<City> source)
        {
            List<CityDTO> cities = new List<CityDTO>();
            foreach(var s in source)
            {
                cities.Add(FromCityToCityDTO(s));
            }
            return cities;
        }

        public static MessageHeaderTypeDTO FromMessageHeaderTypeToMessageHeaderTypeDTO(MessageHeaderType source)
        {
            return _fromMessageHeaderTypeToMessageHeaderTypeDTO.CreateMapper().Map<MessageHeaderType, MessageHeaderTypeDTO>(source);
        }
        public static List<MessageHeaderTypeDTO> FromMessageHeaderTypeToMessageHeaderTypeDTO(IEnumerable<MessageHeaderType> source)
        {
            List<MessageHeaderTypeDTO> messageHeaders = new List<MessageHeaderTypeDTO>();
            foreach(var s in source)
            {
                messageHeaders.Add(FromMessageHeaderTypeToMessageHeaderTypeDTO(s));
            }
            return messageHeaders;
        }

        public static RelationshipDTO FromRelationshipToRelationshipDTO(Relationship source)
        {
            return _fromRelationshipToRelationshipDTO.CreateMapper().Map<Relationship, RelationshipDTO>(source);
        }

        public static List<RelationshipDTO> FromRelationshipToRelationshipDTO(IEnumerable<Relationship> source)
        {
            List<RelationshipDTO> relationships = new List<RelationshipDTO>();
            foreach(var s in source)
            {
                relationships.Add(FromRelationshipToRelationshipDTO(s));
            }
            return relationships;
        }



        private static int Max(params int[] numbers)
        {
            int max = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (max < numbers[i])
                {
                    max = numbers[i];
                }
            }
            return max;
        }
    }
}
