using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.DTOS.Inputs.FriendRequist
{
    public class ResponseFriendRequistDto
    {
        public string UserNmae { get; set; }
        public Guid Id { get; set; }
        public Boolean IsFriend { get; set; }
    }
}
