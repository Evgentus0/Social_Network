using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IClientProfileManager Profiles { get; }
        IMessageHeaderRepository MessageHeaders { get; }
        IMessageRepository Messages { get; }
        IPublicationRepository Publications { get; }
        IAdditionalRepository Additionals { get; }
        void Save();
    }
}
