using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork_BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;

namespace SocialNetwork_BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _dataBase;
        public UserService(IUnitOfWork dataBase)
        {
            _dataBase = dataBase;
        }
        public async Task<ClaimsIdentity> AuthenticateAsync(UserLoginDTO user)
        {
            ClaimsIdentity claim = null;
            ApplicationUser appUser = _dataBase.UserManager.Find(user.Email, user.Password);
            if (appUser != null)
            {
                var profile = _dataBase.Profiles.GetById(appUser.Id);
                if (profile.IsBlocked == true)
                {
                    throw new Exception("this user is blocked!");
                }
                claim = await _dataBase.UserManager.CreateIdentityAsync(appUser,
                                            OAuthDefaults.AuthenticationType);
            }
            return claim;
        }


        public void BlockUser(string id)
        {
            var user = _dataBase.Profiles.GetById(id);
            if (user == null)
                throw new ArgumentNullException("User doesn't exist");
            user.IsBlocked=true;
            _dataBase.Profiles.Update(user);
            _dataBase.Save();
        }

        public async Task<IdentityResult> CreateAsync(UserDTO user)
        {
            ApplicationUser appUser = await _dataBase.UserManager.FindByEmailAsync(user.Email);
            if (appUser == null)
            {
                appUser = new ApplicationUser()
                {
                    Email = user.Email,
                    UserName = user.Email,
                    PhoneNumber=user.PhoneNumber
                    
                };
                var result = await _dataBase.UserManager.CreateAsync(appUser, user.Password);
                if (result.Errors.Count() > 0)
                {
                    return result;
                }
                _dataBase.UserManager.AddToRole(appUser.Id, user.Role);
                ClientProfile clientProfile = new ClientProfile()
                {
                    Id = appUser.Id,
                    Name = user.Name,
                    Surname=user.Surname,
                    Gender=user.Gender,
                    DateOfBirth=user.DateOfBirth,
                    PersonalInfo=user.PersonalInfo,
                    RelationshipId=user.Relationship.Id,
                    CountryId=user.Country.Id,
                    CityId=user.City.Id,
                    Publications=null,
                    PictureProfilePath=user.PictureProfilePath,
                    Followers=null,
                    Following=null,
                    IsBlocked=user.IsBlocked,
                    MessageHeaders=null,
                };
                _dataBase.Profiles.Create(clientProfile);
                _dataBase.Save();
                return IdentityResult.Success;
            }
            else
            {
                return new IdentityResult("This email has already used");
            }
        }

        public IdentityResult Delete(string id)
        {
            var appUser = _dataBase.UserManager.FindById(id);
            if (appUser == null)
                throw new ArgumentNullException("User doesn't exist");
            if (_dataBase.RoleManager.FindById(appUser.Roles.First().RoleId).Name == "Moderator")
            {
                throw new MethodAccessException("this user is moderator");
            }
            _dataBase.Profiles.Delete(id);
            var result= _dataBase.UserManager.Delete(appUser);
            _dataBase.Save();
            return result;
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }

        public ICollection<UserDTO> GetFollowersById(string id)
        {
            var followers = _dataBase.Profiles.GetByIdWithFollowers(id).Followers;
            var dto= CustomMapperBLL.FromClientProfileToUserDTO(followers);
            return dto;
        }

        public ICollection<UserDTO> GetFollowingById(string id)
        {
            var following = _dataBase.Profiles.GetByIdWithFollowing(id).Following;
            return CustomMapperBLL.FromClientProfileToUserDTO(following);
        }

        

        public UserDTO GetUserByEmail(string email)
        {
            var appUser = _dataBase.UserManager.FindByEmail(email);
            var userDto = CustomMapperBLL.FromClientProfileToUserDTO(_dataBase.Profiles.GetById(appUser.Id), false);
            userDto.Email = appUser.Email;
            userDto.Role = _dataBase.RoleManager.FindById(appUser.Roles.First().RoleId).Name;
            userDto.PhoneNumber = appUser.PhoneNumber;

            return userDto;
        }

        public UserDTO GetUserById(string id)
        {
            var appUser = _dataBase.UserManager.FindById(id);
            var userDto = CustomMapperBLL.FromClientProfileToUserDTO(_dataBase.Profiles.GetById(id), false);
            userDto.Email = appUser.Email;
            userDto.PhoneNumber = appUser.PhoneNumber;
            return userDto;
        }

        public void Subscribe(string email, string id)
        {
            var app = _dataBase.UserManager.FindByEmail(email);
            var user = _dataBase.Profiles.GetById(app.Id);
            user.Following.Add(_dataBase.Profiles.GetById(id));
            _dataBase.Profiles.Update(user);
            _dataBase.Save();
        }

        public void UnSubscribe(string email, string id)
        {
            var app = _dataBase.UserManager.FindByEmail(email);
            var user = _dataBase.Profiles.GetByIdWithFollowing(app.Id);
            var follower= _dataBase.Profiles.GetById(id);
            user.Following.Remove(follower);
            _dataBase.Profiles.Update(user);
            _dataBase.Save();
        }

        public ICollection<UserDTO> GetAll()
        {
            var users = _dataBase.Profiles.GetAll();
            return CustomMapperBLL.FromClientProfileToUserDTO(users);
        }

        public ICollection<UserDTO> GetUsersByCity(string city)
        {
            var users = _dataBase.Profiles.Find(x => x.City.Name == city);
            return CustomMapperBLL.FromClientProfileToUserDTO(users);
        }
    }
}
