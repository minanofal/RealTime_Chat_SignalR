using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Inputs.Message
{
    public class MessageInputDto
    {
        public string MessageText { get; set; }
        public string? SenderUserName { get; set; }
        public Guid ConnectionId { get; set; }

    }
}
