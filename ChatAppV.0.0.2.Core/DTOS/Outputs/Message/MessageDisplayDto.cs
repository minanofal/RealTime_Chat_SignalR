using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Outputs.Message
{
    public class MessageDisplayDto
    {
        
        public Guid Id { get; set; }
        public Guid ConnectionId { get; set; }
        public string SenderUserName { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Seen { get; set; }
        

    }
} 
