using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork_DAL.EF;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Identity;
using SocialNetwork_DAL.Interfaces;
using System;

namespace SocialNetwork_DAL.Repositories
{
    public  class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private SocialNetworkContext _context;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientProfileManager _clientProfileManager;
        private IMessageHeaderRepository _messageHeaderRepository;
        private IMessageRepository _messageRepository;
        private IPublicationRepository _publicationRepository;
        private IAdditionalRepository _additionalRepository;


        public EFUnitOfWork(string connectionString)
        {
            _context = new SocialNetworkContext(connectionString);
        }

        public IAdditionalRepository Additionals
        {
            get
            {
                if (_additionalRepository == null)
                    _additionalRepository = new AdditionalRepository(_context);
                return _additionalRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                    _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
                return _roleManager;
            }
        }

        public IClientProfileManager Profiles
        {
            get
            {
                if (_clientProfileManager == null)
                    _clientProfileManager = new ClientProfileManager(_context);
                return _clientProfileManager;
            }
        }

        public IMessageHeaderRepository MessageHeaders
        {
            get
            {
                if (_messageHeaderRepository == null)
                    _messageHeaderRepository = new MessageHeaderRepository(_context);
                return _messageHeaderRepository;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_context);
                return _messageRepository;
            }
        }

        public IPublicationRepository Publications
        {
            get
            {
                if (_publicationRepository == null)
                    _publicationRepository = new PublicationRepository(_context);
                return _publicationRepository;
            }
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }


        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
