using ChatAppV._0._0._2.Core.Models.ChatModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Models.Auithentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public char Gender { get; set; }
        public ICollection<Connection> User1Connection{ get; set; }
        public ICollection<Connection> User2Connection { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<FriendRequist> SenderFriendRequists { get; set; }
        public ICollection<FriendRequist> ResiverFriendRequists { get; set; }
    }
}
