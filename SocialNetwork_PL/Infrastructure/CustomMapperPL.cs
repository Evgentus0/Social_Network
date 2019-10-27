using AutoMapper;
using SocialNetwork_BLL.DTO;
using SocialNetwork_PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork_PL.Infrastructure
{
    public class CustomMapperPL
    {
        private static MapperConfiguration _fromPublicationDtoToPublicationModel;
        private static MapperConfiguration _fromUserDtoToUserModel;
       // private static MapperConfiguration _fromMessageDtoToMessageModel;
        private static MapperConfiguration _fromMessageHeaderDtoToMessageHeaderModel;
        private static MapperConfiguration _fromCountryDtoToCountryModel;
        private static MapperConfiguration _fromCityDtoToCityModel;
        private static MapperConfiguration _fromMessageHeaderTypeDtoToMessageHeaderTypeModel;
        private static MapperConfiguration _fromRelationshipDtoToRelationshipModel;

        static CustomMapperPL()
        {
            _fromPublicationDtoToPublicationModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PublicationDTO, PublicationModel>().ForMember(d => d.UsersWhoLike, a => a.Ignore());
                cfg.CreateMap<UserDTO, UserModel>()
                  .ForMember(d => d.Publications, a => a.Ignore())
                .ForMember(d => d.LikedPublications, a => a.Ignore())
                .ForMember(d => d.Followers, a => a.Ignore())
                .ForMember(d => d.Following, a => a.Ignore())
                .ForMember(d => d.MessageHeaders, a => a.Ignore());
            });
            _fromUserDtoToUserModel = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<UserDTO, UserModel>()
                  .ForMember(d => d.Publications, a => a.Ignore())
                .ForMember(d => d.LikedPublications, a => a.Ignore())
                .ForMember(d => d.Followers, a => a.Ignore())
                .ForMember(d => d.Following, a => a.Ignore())
                .ForMember(d => d.MessageHeaders, a => a.Ignore());
                  cfg.CreateMap<CityDTO, CityModel>();
                  cfg.CreateMap<CountryDTO, CountryModel>();
                  cfg.CreateMap<RelationshipDTO, RelationshipModel>();
              });
            //_fromMessageDtoToMessageModel = new MapperConfiguration(cfg =>
            //  {
            //      cfg.CreateMap<MessageDTO, MessageModel>()
            //      .ForMember(d => d.DeleteFor, a => a.Ignore())
            //        .ForMember(d => d.MessageHeader, s => s.MapFrom(src => src.MessageHeader))
            //        .ForMember(d => d.Sender, s => s.MapFrom(src => src.Sender))
            //        .ForMember(d => d.Content, s => s.MapFrom(src => src.Content))
            //        .ForMember(d => d.DateTimeSend, s => s.MapFrom(src => src.DateTimeSend))
            //        .ForMember(d => d.IsRead, s => s.MapFrom(src => src.IsRead))
            //        .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
            //        ;
            //      cfg.CreateMap<MessageHeaderDTO, MessageHeaderModel>()
            //      .ForMember(d => d.Users, a => a.Ignore())
            //   .ForMember(d => d.Messages, a => a.Ignore());
            //      cfg.CreateMap<UserDTO, UserModel>()
            //      .ForMember(d => d.Publications, a => a.Ignore())
            //    .ForMember(d => d.LikedPublications, a => a.Ignore())
            //    .ForMember(d => d.Followers, a => a.Ignore())
            //    .ForMember(d => d.Following, a => a.Ignore())
            //    .ForMember(d => d.MessageHeaders, a => a.Ignore());
            //      cfg.CreateMap<MessageHeaderTypeDTO, MessageHeaderTypeModel>();
            //  });
            _fromMessageHeaderDtoToMessageHeaderModel = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<MessageHeaderDTO, MessageHeaderModel>()
                   .ForMember(d => d.Users, a => a.Ignore())
                .ForMember(d => d.Messages, a => a.Ignore());
                  cfg.CreateMap<MessageHeaderTypeDTO, MessageHeaderTypeModel>();
              });

            _fromCountryDtoToCountryModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CountryDTO, CountryModel>();
            });
            _fromCityDtoToCityModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CityDTO, CityModel>();
                cfg.CreateMap<CountryDTO, CountryModel>();
            });
            _fromMessageHeaderTypeDtoToMessageHeaderTypeModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageHeaderTypeDTO, MessageHeaderTypeModel>();
            });
            _fromRelationshipDtoToRelationshipModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RelationshipDTO, RelationshipModel>();
            });
        }

        public static PublicationModel FromPublicationDtoToPublicationModel(PublicationDTO source, bool isRecurtion)
        {
            var publication= _fromPublicationDtoToPublicationModel.CreateMapper().Map<PublicationDTO, PublicationModel>(source);
            if (!isRecurtion)
            {
                foreach (var p in source.UsersWhoLike)
                {
                    publication.UsersWhoLike.Add(FromUserDtoToUserModel(p, true));
                }
            }
            return publication;
        }
        public static List<PublicationModel> FromPublicationDtoToPublicationModel(ICollection<PublicationDTO> source)
        {
            List<PublicationModel> publications = new List<PublicationModel>();
            foreach(var s in source)
            {
                publications.Add(FromPublicationDtoToPublicationModel(s, false));
            }
            return publications;
        }

        public static UserModel FromUserDtoToUserModel(UserDTO source, bool isRecurtion)
        {
            var user = _fromUserDtoToUserModel.CreateMapper().Map<UserDTO, UserModel>(source);
            if (!isRecurtion)
            {
                int maxLength = Max(source.Publications.Count, source.LikedPublications.Count, source.Followers.Count, source.Following.Count, source.MessageHeaders.Count);
                for (int i = 0; i < maxLength; i++)
                {
                    if (i < user.Publications.Count)
                    {
                        user.Publications.Add(FromPublicationDtoToPublicationModel(source.Publications.ElementAt(i), true));
                    }
                    if (i < user.LikedPublications.Count)
                    {
                        user.LikedPublications.Add(FromPublicationDtoToPublicationModel(source.LikedPublications.ElementAt(i), true));
                    }
                    if (i < user.Followers.Count)
                    {
                        user.Followers.Add(FromUserDtoToUserModel(source.Followers.ElementAt(i), true));
                    }
                    if (i < user.Following.Count)
                    {
                        user.Following.Add(FromUserDtoToUserModel(source.Following.ElementAt(i), true));
                    }
                    if (i < user.MessageHeaders.Count)
                    {
                        user.MessageHeaders.Add(FromMessageHeaderDtoToMessageHeaderModel(source.MessageHeaders.ElementAt(i), true));
                    }
                }
            }
            return user;
        }
        public static List<UserModel> FromUserDtoToUserModel(ICollection<UserDTO> source)
        {
            List<UserModel> users = new List<UserModel>();
            foreach(var s in source)
            {
                users.Add(FromUserDtoToUserModel(s, false));
            }
            return users;
        }

        public static MessageModel FromMessageDtoToMessageModel(MessageDTO source, bool isRecurtion)
        {
            //var message = _fromMessageDtoToMessageModel.CreateMapper().Map<MessageDTO, MessageModel>(source);
            MessageModel message = new MessageModel()
            {
                Id = source.Id,
                Content = source.Content,
                IsRead = source.IsRead,
                DateTimeSend = source.DateTimeSend,
                MessageHeader = FromMessageHeaderDtoToMessageHeaderModel(source.MessageHeader, true),
                Sender = FromUserDtoToUserModel(source.Sender, true)
            };

            if (!isRecurtion)
            {
                foreach (var p in source.DeleteFor)
                {
                    message.DeleteFor.Add(FromUserDtoToUserModel(p, true));
                }
            }
            return message;
        }
        public static List<MessageModel> FromMessageDtoToMessageModel(ICollection<MessageDTO> source)
        {
            List<MessageModel> messages = new List<MessageModel>();
            foreach(var s in source)
            {
                messages.Add(FromMessageDtoToMessageModel(s, false));
            }
            return messages;
        }

        public static MessageHeaderModel FromMessageHeaderDtoToMessageHeaderModel(MessageHeaderDTO source, bool isRecurtion)
        {
            var messageHeader = _fromMessageHeaderDtoToMessageHeaderModel.CreateMapper().Map<MessageHeaderDTO, MessageHeaderModel>(source);
            if (!isRecurtion)
            {
                foreach (var u in source.Users)
                {
                    messageHeader.Users.Add(FromUserDtoToUserModel(u, true));
                }
                foreach (var m in source.Messages)
                {
                    messageHeader.Messages.Add(FromMessageDtoToMessageModel(m, true));
                }
            }
            return messageHeader;
        }
        public static List<MessageHeaderModel> FromMessageHeaderDtoToMessageHeaderModel(ICollection<MessageHeaderDTO> source)
        {
            List<MessageHeaderModel> messageHeaders = new List<MessageHeaderModel>();
            foreach(var s in source)
            {
                messageHeaders.Add(FromMessageHeaderDtoToMessageHeaderModel(s, false));
            }
            return messageHeaders;
        }

        public static CountryModel FromCountryDtoToCountryModel(CountryDTO source)
        {
            return _fromCountryDtoToCountryModel.CreateMapper().Map<CountryDTO, CountryModel>(source);
        }
        public static List<CountryModel> FromCountryDtoToCountryModel(ICollection<CountryDTO> source)
        {
            List<CountryModel> countries = new List<CountryModel>();
            foreach (var s in source)
            {
                countries.Add(FromCountryDtoToCountryModel(s));
            }
            return countries;
        }

        public static CityModel FromCityDtoToCityModel(CityDTO source)
        {
            return _fromCityDtoToCityModel.CreateMapper().Map<CityDTO, CityModel>(source);
        }
        public static List<CityModel> FromCityDtoToCityModel(ICollection<CityDTO> source)
        {
            List<CityModel> cities = new List<CityModel>();
            foreach (var s in source)
            {
                cities.Add(FromCityDtoToCityModel(s));
            }
            return cities;
        }

        public static MessageHeaderTypeModel FromMessageHeaderTypeDtoToMessageHeaderTypeModel(MessageHeaderTypeDTO source)
        {
            return _fromMessageHeaderTypeDtoToMessageHeaderTypeModel.CreateMapper().Map<MessageHeaderTypeDTO, MessageHeaderTypeModel>(source);
        }
        public static List<MessageHeaderTypeModel> FromMessageHeaderTypeDtoToMessageHeaderTypeModel(IEnumerable<MessageHeaderTypeDTO> source)
        {
            List<MessageHeaderTypeModel> messageHeaders = new List<MessageHeaderTypeModel>();
            foreach (var s in source)
            {
                messageHeaders.Add(FromMessageHeaderTypeDtoToMessageHeaderTypeModel(s));
            }
            return messageHeaders;
        }

        public static RelationshipModel FromRelationshipDtoToRelationshipModel(RelationshipDTO source)
        {
            return _fromRelationshipDtoToRelationshipModel.CreateMapper().Map<RelationshipDTO, RelationshipModel>(source);
        }

        public static List<RelationshipModel> FromRelationshipDtoToRelationshipModel(IEnumerable<RelationshipDTO> source)
        {
            List<RelationshipModel> relationships = new List<RelationshipModel>();
            foreach (var s in source)
            {
                relationships.Add(FromRelationshipDtoToRelationshipModel(s));
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