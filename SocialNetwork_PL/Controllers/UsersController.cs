using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SocialNetwork_BLL.DTO;
using SocialNetwork_BLL.Interfaces;
using SocialNetwork_PL.Infrastructure;
using SocialNetwork_PL.Models;
using SocialNetwork_PL.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SocialNetwork_PL.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDTO
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    PersonalInfo = model.PersonalInfo,
                    Relationship = new RelationshipDTO { Id = model.RelationShipId },
                    Country = new CountryDTO { Id = model.CountryId },
                    City = new CityDTO { Id = model.CityId },
                    PictureProfilePath = model.PictureProfilePath,
                    IsBlocked = false,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Role="User"
                };
                var result = await _userService.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                return Ok("You register successful");
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("block")]
        public IHttpActionResult BlockUser([FromBody]string id)
        {
            _userService.BlockUser(id);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles ="Moderator")]
        [Route("{id}")]
        public IHttpActionResult DleteUser(string id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var users = _userService.GetAll();
            var models = CustomMapperPL.FromUserDtoToUserModel(users);
            return Ok(models);
        }

        [HttpGet]
        [Route("followers/{id}")]
        public IHttpActionResult GetFollowers(string id)
        {
            var users = CustomMapperPL.FromUserDtoToUserModel(_userService.GetFollowersById(id));
            return Ok(users);
        }
        [HttpGet]
        [Route("following/{id}")]
        public IHttpActionResult GetFollowing(string id)
        {
            var users = CustomMapperPL.FromUserDtoToUserModel(_userService.GetFollowingById(id));
            return Ok(users);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCurrentUser()
        {
            var user = CustomMapperPL.FromUserDtoToUserModel(_userService.GetUserByEmail(User.Identity.Name), false);
            return Ok(user);
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult GetUser(string id)
        {
            var user = CustomMapperPL.FromUserDtoToUserModel(_userService.GetUserById(id), false);
            return Ok(user);
        }

        [HttpPut]
        [Route("subscribe")]
        public IHttpActionResult Subscribe([FromBody] string id)
        {
            _userService.Subscribe(User.Identity.Name, id);
            return Ok();
        }

        [HttpPut]
        [Route("unsubscribe")]
        public IHttpActionResult UnSubscribe([FromBody] string id)
        {
            _userService.UnSubscribe(User.Identity.Name, id);
            return Ok();
        }

        [HttpGet]
        [Route("GetByCity/{city}")]
        [AllowAnonymous]
        public IHttpActionResult GetCountOfUsersInCity(string city)
        {
            var users = CustomMapperPL.FromUserDtoToUserModel(_userService.GetUsersByCity(city));
            return Ok(users);
        }

        [HttpGet]
        [Route("search")]
        [AllowAnonymous]
        public IHttpActionResult GetFilteredUsers()
        {
            var users = _userService.GetAll();
            var models = CustomMapperPL.FromUserDtoToUserModel(users).Select(user => new UserSearch() { id=user.Id, name=user.Name}).ToList();
            return Ok(new SearchResult() {results=models, total=models.Count() });
        }

        #region helpers
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }
        #endregion

    }
}
