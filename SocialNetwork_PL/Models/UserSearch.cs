using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork_PL.Models
{
    public class UserSearch
    {
        public string id;
        public string name;
    }

    public class SearchResult
    {
        public int total;
        public List<UserSearch> results;

        public SearchResult()
        {
            results = new List<UserSearch>();
        }
    }
}