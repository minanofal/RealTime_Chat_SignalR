using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Inputs.FriendRequist;
using ChatAppV._0._0._2.Core.DTOS.Outputs.Authentication;
using ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.Core.Models.ChatModels;
using ChatAppV._0._0._2.EF.Repositories.UnitOFWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.ChatRepository
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        public IUnitOFWorkRepository _unitOfWork { get; private set; }
        private readonly IMapper _mapper;

        public FriendRequestRepository(ApplicationDbContext context, IMapper mapper)
        {
           _unitOfWork = new UnitOFWorkRepository(context);
            _mapper = mapper;
           
           
        }

        

        public async Task<string> SendFriendRequist(SendFrindRequestDto dto)
        {

            if (dto.Sendername == dto.Resivername)
                return "Can't send FriendRequest to Your self !";

            var sender = await _unitOfWork.Users.GetAsyncTraking(u=>u.UserName == dto.Sendername);
            if (sender == null)
                return $"{dto.Sendername} is not exist";
        
            var resiver = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == dto.Resivername);
            if (resiver == null)
                return $"{dto.Resivername} is not exist";

            var checkSent = await  _unitOfWork.FriendRequists.GetAsyncTraking(R => 
                (R.SenderId == sender.Id && R.ResiverId == resiver.Id) || (R.SenderId == resiver.Id  && R.ResiverId == sender.Id));
            if( checkSent != null)
                return "The Friend Request already sent";

            var checkFriend = await _unitOfWork.Connections.GetAsyncTraking(C => (C.User1ID == sender.Id && C.User2ID == resiver.Id)||
                (C.User1ID == resiver.Id  && C.User2ID == sender.Id));
            if (checkFriend != null)
                return $"{dto.Sendername} and {dto.Resivername} already Friends";

            var frindRequest = new FriendRequist() { ResiverId = resiver.Id, SenderId = sender.Id };
            var result = await _unitOfWork.FriendRequists.CreateAsync(frindRequest);
            if (result == null)
                return "SomeThing went wrong";

            return string.Empty;
        }

        public async Task<string> CancelFriendRequist(SendFrindRequestDto dto)
        {
            if (dto.Sendername == dto.Resivername)
                return "Can't send FriendRequest to Your self !";

            var sender = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == dto.Sendername);
            if (sender == null)
                return $"{dto.Sendername} is not exist";

            var resiver = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == dto.Resivername);
            if (resiver == null)
                return $"{dto.Resivername} is not exist";

            var checkSent = await _unitOfWork.FriendRequists.GetAsyncTraking(R => R.SenderId == sender.Id && R.ResiverId == resiver.Id);
            if (checkSent == null)
                return "There is no Friend Request sent";

            var checkFriend = await _unitOfWork.Connections.GetAsyncTraking(C => (C.User1ID == sender.Id && C.User2ID == resiver.Id) ||
                (C.User1ID == resiver.Id && C.User2ID == sender.Id));
            if (checkFriend != null)
                return $"{dto.Sendername} and {dto.Resivername} already Friends";

            var result = await _unitOfWork.FriendRequists.DeleteAsync(R => R.SenderId == sender.Id && R.ResiverId == resiver.Id);
            if (!result)
                return "SomeThing Went Wrong";
            return string.Empty;
        }

        public async Task<IEnumerable<DisplayUserFrindRequistDto>> GetFrindRequists(string userName)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == userName);
            if (user == null)
                return null;
            var requists = await _unitOfWork.FriendRequists.GetAllAsyncNoTraking(R => R.ResiverId == user.Id, new[] { "Sender" });
            var senders = requists.Select(R => new DisplayUserFrindRequistDto()
            {
                Email = R.Sender.Email,
                FirstName = R.Sender.FirstName,
                LastName = R.Sender.LastName,
                ImageUrl = R.Sender.ImageUrl,
                UserName = R.Sender.UserName,
                Id = R.Id
            }) ;
            return senders;
        }

        public async Task<string> ResponseFriendRequist(ResponseFriendRequistDto dto)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == dto.UserNmae);
            if (user == null)
                return $"{dto.UserNmae} is not exist";


            var requist = await _unitOfWork.FriendRequists.GetAsyncTraking(R => R.ResiverId == user.Id && R.Id == dto.Id);
            if (requist == null)
                return "There is No FriendRequists";

            var connectionCheck = await _unitOfWork.Connections.GetAsyncTraking(C => 
                (C.User1ID == user.Id && C.User2ID == requist.SenderId) || (C.User1ID == requist.SenderId && C.User2ID == user.Id)
                );
            if (connectionCheck != null)
                return "You Already Friends";


            if (dto.IsFriend)
            {
                var connection = await _unitOfWork.Connections.CreateAsync(new 
                    Connection() { User1ID = user.Id, User2ID = requist.SenderId });
                if (connection == null)
                    return "Some Thing went Wrong";
            }

            var deleteresult = await _unitOfWork.FriendRequists.DeleteAsync(requist);
            if(!deleteresult)
                return "Some Thing went Wrong";

            return String.Empty;

        }

        public async Task<IEnumerable<DisplayUserFrindRequistDto>> GetMyFrindRequists(string userName)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(u => u.UserName == userName);
            if (user == null)
                return null;
            var requists = await _unitOfWork.FriendRequists.GetAllAsyncNoTraking(R => R.SenderId == user.Id, new[] { "Resiver" });
            var senders = requists.Select(R => new DisplayUserFrindRequistDto()
            {
                Email = R.Resiver.Email,
                FirstName = R.Resiver.FirstName,
                LastName = R.Resiver.LastName,
                ImageUrl = R.Resiver.ImageUrl,
                UserName = R.Resiver.UserName,
                Id = R.Id
            });
            return senders;
        }

    }
}
