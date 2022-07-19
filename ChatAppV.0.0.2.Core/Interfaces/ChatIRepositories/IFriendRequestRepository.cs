using ChatAppV._0._0._2.Core.DTOS.Inputs.FriendRequist;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories
{
    public interface IFriendRequestRepository
    {
        IUnitOFWorkRepository _unitOfWork { get; }
        Task<string> SendFriendRequist(SendFrindRequestDto dto);
        Task<string> CancelFriendRequist(SendFrindRequestDto dto);
        Task<IEnumerable<DisplayUserFrindRequistDto>> GetFrindRequists(string userName);
        Task<IEnumerable<DisplayUserFrindRequistDto>> GetMyFrindRequists(string userName);
        Task<string> ResponseFriendRequist(ResponseFriendRequistDto dto);
    }
}
