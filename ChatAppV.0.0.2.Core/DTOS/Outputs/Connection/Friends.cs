using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Inputs.Connection
{
    public class Friends
    {
        public Guid ConnectionId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string LastMessage { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }
        public string SenderUserName { get; set; }
        public int UnseenMessagesCount { get; set; }
    }
}
