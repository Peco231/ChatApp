using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class UserSearchResult
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public UserSearchResult(string id, string fullname)
        {
            Id = id;
            FullName = fullname;
        }
    }
}
