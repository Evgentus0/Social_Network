using SocialNetwork_DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.Interfaces
{
    public interface IPublicationRepository
    {
        void Create(Publication item);
        Publication GetById(int id);
        IEnumerable<Publication> GetAll();
        void Update(Publication item);
        void Delete(int id);
        IEnumerable<Publication> Find(Func<Publication, bool> predicate);
    }
}
